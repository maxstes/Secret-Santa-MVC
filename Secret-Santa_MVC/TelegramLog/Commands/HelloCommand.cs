using Secret_Santa_MVC.TelegramLog.Data;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Secret_Santa_MVC.TelegramLog.Commands
{
    public class HelloCommand : BaseCommand
    {
        private readonly TelegramBotClient _client;
        private readonly ILogger<HelloCommand> _logger;

        public HelloCommand(Bot telegramBot, ILogger<HelloCommand> logger)
        {
            _client = telegramBot.Get().Result;
            _logger = logger;
        }
        public override string Name => CommandNames.HelloCommand;

        public override void Execute(Message message)
        {
            
            CommandModel model = new CommandModel();

             model.chatId = message.Chat.Id;
            model.messageId = message.MessageId;

            //tryed withraw via bot
            _logger.LogCritical("Test crit"); 



            _client.SendTextMessageAsync(model.chatId, "Hello!", replyToMessageId:model.messageId);
        }
    }
}
