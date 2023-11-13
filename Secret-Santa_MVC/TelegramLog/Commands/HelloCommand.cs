using Secret_Santa_MVC.TelegramLog.Data;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Secret_Santa_MVC.TelegramLog.Commands
{
    public class HelloCommand : Command
    {
        public override string Name => "hello";

        public override void Execute(Message message, TelegramBotClient client)
        {
            CommandModel model = new CommandModel();

            model.chatId = message.Chat.Id;
            model.messageId = message.MessageId;

            //TODO: Command logic -_-

            client.SendTextMessageAsync(model.chatId, "Hello!", replyToMessageId:model.messageId);
        }
    }
}
