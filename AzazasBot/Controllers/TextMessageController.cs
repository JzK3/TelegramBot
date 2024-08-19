using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using AzazasBot.Config;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace AzazasBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        public static string command;

        public TextMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(Message message,  CancellationToken ct)
        {
            Console.WriteLine($"{GetType().Name} получил сообщение \"{message.Text}\" от {message.Chat.FirstName ?? "АНОН КАКОЙ-ТО"}");

            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"длина" , $"lg"),
                        InlineKeyboardButton.WithCallbackData($"сумма" , $"sm")
                    });
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                        $"<b> Наш бот считает длину сообщения или сумму введенных цифр</b> {Environment.NewLine}" +
                        $"Выбери действие, которое хотешь совершить над сообщением{Environment.NewLine}", cancellationToken: ct,
                        parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    switch (command)
                    {
                        case "sm":
                            await _telegramClient.SendTextMessageAsync(message.From.Id, $"Сумма чисел: {Calculator.Calc(message.Text)} ");
                            break;
                        case "lg":
                            await _telegramClient.SendTextMessageAsync(message.From.Id, $"Длина сообщения: {message.Text.Length} знаков");
                            break;
                    }
                    break;
            }
        }

    }
}
