using Secret_Santa_MVC.TelegramLog.Data;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Secret_Santa_MVC.TelegramLog.Commands
{
    public class InvationCommand : BaseCommand
    {
        public override string Name => CommandNames.InvationCommand;

        public override void Execute(Message message, TelegramBotClient client)
        {
            CommandModel commandModel = new();
            //commandModel.messageId = message.MessageId;
            commandModel.chatId = message.Chat.Id;
            string botMessage = "Hello, thanks for the invitation";


            client.SendTextMessageAsync(commandModel.chatId,botMessage);
        }
    }
}
