namespace Secret_Santa_MVC.TelegramLog.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Secret_Santa_MVC.TelegramLog.Commands;
    using System;
    using Telegram.Bot;

    public class TelegramLoggerProvider : ILoggerProvider
    {
        private TelegramBotClient _bot = new Bot().Get().Result;

        public ILogger CreateLogger(string categoryName)
        {
            return new TelegramLogger(_bot);
        }

        public void Dispose() { }
    }

}
