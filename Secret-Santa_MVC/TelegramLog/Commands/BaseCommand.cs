using Org.BouncyCastle.Asn1.Cmp;
using Secret_Santa_MVC.TelegramLog.Data;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Secret_Santa_MVC.TelegramLog.Commands
{
    public abstract class BaseCommand
    {
        public abstract string Name { get; }

        public abstract void Execute(Message message, TelegramBotClient client);
        public bool Contains(string command)
        {
            return command.Contains(this.Name) || command.Contains(AppSettings.Name);
        }
        

    }
}
