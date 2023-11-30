using Secret_Santa_MVC.TelegramLog.Data;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Secret_Santa_MVC.TelegramLog.Commands
{
    public class StatusCommand : BaseCommand
    {
        public override string Name => CommandNames.StatusCommand;

        public override void Execute(Message message, TelegramBotClient client)
        {
            CommandModel model = new();

            model.chatId = message.Chat.Id;
            model.messageId = message.MessageId;

            client.SendTextMessageAsync(model.chatId,"Hello, bot working",replyToMessageId:model.messageId);
        }
    }
}
