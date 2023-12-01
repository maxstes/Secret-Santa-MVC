﻿using Secret_Santa_MVC.TelegramLog.Data;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;
using X.Extensions.Logging.Telegram;
using NgrokExtensions;

namespace Secret_Santa_MVC.TelegramLog.Commands
{
    public class HelloCommand : BaseCommand
    {
        private readonly TelegramBotClient _client;

        public HelloCommand(Bot telegramBot)
        {
            _client = telegramBot.Get().Result;
        }
        public override string Name => CommandNames.HelloCommand;

        public override void Execute(Message message)
        {
            
            CommandModel model = new CommandModel();

            model.chatId = message.Chat.Id;
            model.messageId = message.MessageId;
            
            //tryed withraw via bot
            //TODO: Command logic -_-
            //var options = new TelegramLoggerOptions
            //{
            //    AccessToken = "",
            //    ChatId = Convert.ToString(model.chatId),
            //    //LogLevel = LogEventLevel.Warning,
            //    Source = "human readable project name", 
            //};
            //var factory = LoggerFactory.Create(builder =>
            //{
            //    builder
            //        .ClearProviders()
            //        .AddTelegram(options)
            //        .AddConsole();
            //});
            //var logg = factory.CreateLogger<HelloCommand>();
            //logg.LogCritical("Alo");
            

            _client.SendTextMessageAsync(model.chatId, "Hello!", replyToMessageId:model.messageId);
        }
    }
}
