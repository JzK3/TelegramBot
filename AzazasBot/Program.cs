using AzazasBot.Config;
using AzazasBot.Controllers;
using AzazasBot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;

namespace AzazasBot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "7390852043:AAF222X1VjIlLs_t5yExvvPGL2hdWQluLvM"
            };
        }

        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings app = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());
            services.AddSingleton<IStorage, MemStorage>();
            services.AddTransient<DefaultMessageController>();
            services.AddTransient <InlineKeyBoardController>();
            services.AddTransient <TextMessageController>();
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(app.BotToken));
            services.AddHostedService<Bot>();
        }
    }
}
