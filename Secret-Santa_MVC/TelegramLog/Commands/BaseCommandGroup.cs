using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Secret_Santa_MVC.TelegramLog.Commands
{
    public class BaseCommandGroup
    {
        public virtual void Execute(MessageType message, TelegramBotClient client)
        {

        }
    }
}
