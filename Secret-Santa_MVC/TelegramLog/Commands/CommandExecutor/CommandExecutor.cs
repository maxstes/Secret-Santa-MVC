using Telegram.Bot.Types;

namespace Secret_Santa_MVC.TelegramLog.Commands.CommandExecutor
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly List<BaseCommand> _commands;

        public CommandExecutor(IServiceProvider serviceProvider)
        {
             _commands = new List<BaseCommand>();
        }

        public async Task Execute(Update update)
        {
            if (update?.Message?.Chat == null && update?.CallbackQuery == null)
                return;
            
        }
    }
}
