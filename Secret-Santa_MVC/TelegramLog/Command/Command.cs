using Telegram.Bot;
using Telegram.Bot.Types;

namespace Secret_Santa_MVC.TelegramLog.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract void Execute(Message message, TelegramBotClient client);

        public bool Contains(string comand)
        {
            return comand.Contains(this.Name) && comand.Contains(AppSettings.Name);
        }
    }
}
