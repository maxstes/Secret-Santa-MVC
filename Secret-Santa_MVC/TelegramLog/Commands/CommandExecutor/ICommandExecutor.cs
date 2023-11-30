using Telegram.Bot.Types;

namespace Secret_Santa_MVC.TelegramLog.Commands.CommandExecutor
{
    public interface ICommandExecutor
    {
        Task Execute(Update update);
    }
}
