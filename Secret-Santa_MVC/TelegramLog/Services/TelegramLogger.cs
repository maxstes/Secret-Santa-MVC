using Telegram.Bot.Types;
using Telegram.Bot;
using Secret_Santa_MVC.TelegramLog.Data;

namespace Secret_Santa_MVC.TelegramLog.Services
{
    public class TelegramLogger : ILogger
    {
        private readonly TelegramBotClient _botClient;
        private readonly LogLevel _logLevel;

        public TelegramLogger(TelegramBotClient botClient, LogLevel logLevel = LogLevel.Information)
        {
            _botClient = botClient;
            _logLevel = logLevel;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel >= _logLevel;

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            var message = formatter(state, exception);
            var logMessage = $"[{logLevel}] {message}";

            // Отправка сообщения в Телеграм
            await _botClient.SendTextMessageAsync(AppSettings.MyChatId, logMessage);
        }
    }
}
