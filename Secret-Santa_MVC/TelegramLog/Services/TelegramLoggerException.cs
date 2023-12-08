namespace Secret_Santa_MVC.TelegramLog.Services
{
    public static class TelegramLoggerException
    {
        public static ILoggingBuilder AddTelegBotLog(this ILoggingBuilder builder)
        {
            builder.AddProvider(new TelegramLoggerProvider());
            return builder;
        }
    }
}
