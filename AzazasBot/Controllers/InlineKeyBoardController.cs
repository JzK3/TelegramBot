using AzazasBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AzazasBot.Controllers
{
    public class InlineKeyBoardController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memStorage;  

        public InlineKeyBoardController(ITelegramBotClient telegramBotClient, IStorage memStorage)
        {
            _telegramClient = telegramBotClient;
            _memStorage = memStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery == null)
                return;
            _memStorage.GetSession(callbackQuery.From.Id).SposobCode = callbackQuery.Data;
            string sposobCode = callbackQuery.Data switch
            {
                "lg" => "длина сообщения",
                "sm" => "сумма цифр",
            };
            Console.WriteLine($"{GetType().Name} получил нажатие на {callbackQuery.Data}");
                await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, $"<b>Выбрана функция - {sposobCode}.{Environment.NewLine}</b>",
                    cancellationToken: ct, parseMode: ParseMode.Html);
            if (sposobCode == "сумма цифр")
            {
                TextMessageController.command = "sm";
            }
            else if (sposobCode == "длина сообщения")
            {
                TextMessageController.command = "lg";
            }

        }

    }
}
