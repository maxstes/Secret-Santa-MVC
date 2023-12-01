using Secret_Santa_MVC.TelegramLog.Data;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Secret_Santa_MVC.TelegramLog.Commands
{
    public class InvationCommand : BaseCommand
    {
        private readonly TelegramBotClient _client;

        public InvationCommand(Bot telegramBot)
        {
            _client = telegramBot.Get().Result;
        }
        public override string Name => CommandNames.InvationCommand;

        public override void Execute(Message message)
        {
            CommandModel commandModel = new();
            //commandModel.messageId = message.MessageId;
            commandModel.chatId = message.Chat.Id;
            string botMessage = "Hello, thanks for the invitation";


            _client.SendTextMessageAsync(commandModel.chatId,botMessage);
        }
    }
}
