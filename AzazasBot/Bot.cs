using AzazasBot.Controllers;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AzazasBot
{
    class Bot : BackgroundService
    {
        private ITelegramBotClient _telegramBotClient;

        private InlineKeyBoardController _inlineKeyBoardController;
        private DefaultMessageController _defaultMessageController;
        private TextMessageController _textMessageController;


        public Bot(ITelegramBotClient telegramBotClient, DefaultMessageController defaultMessageController,
            TextMessageController textMessageController, InlineKeyBoardController inlineKeyBoardController)
        {
            _telegramBotClient = telegramBotClient;
            _defaultMessageController = defaultMessageController;
            _textMessageController = textMessageController;
            _inlineKeyBoardController = inlineKeyBoardController;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _telegramBotClient.StartReceiving(
                    HandleUpdateAsync,
                    HandleErrorAsync,
                    new ReceiverOptions() { AllowedUpdates = { } },
                    cancellationToken: stoppingToken);

                Console.WriteLine("Бот запущен");
        }

            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                await _inlineKeyBoardController.Handle(update.CallbackQuery, cancellationToken);
                return;
            }

            if (update.Type == UpdateType.Message)
            {
                switch (update.Message!.Type)
                {
                    case MessageType.Text:
                        await _textMessageController.Handle(update.Message, cancellationToken);
                        return;
                    default:
                        await _defaultMessageController.Handle(update.Message, cancellationToken);
                        return;
                }
            }
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(errorMessage);
            Console.WriteLine("Ожидаем 10 секунд перед повторным подключением.");
            Thread.Sleep(10000);

            return Task.CompletedTask;
        }

    }
}
