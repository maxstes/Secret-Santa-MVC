using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Secret_Santa_MVC.TelegramLog.Commands.CommandExecutor
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly List<BaseCommand> _commands;

        public CommandExecutor(IServiceProvider serviceProvider)
        {
             _commands = serviceProvider.GetServices<BaseCommand>().ToList();
        }

        public async Task Execute(Update update)
        {
            var message = update.Message;


            await SelectCommand(message, _commands);
        }
        public async Task SelectCommand(Message message, List<BaseCommand> commands)
        {
            foreach (var command in commands)
            {

                if (command.Contains(message.Text))
                {
                    command.Execute(message);
                    break;
                }
            }
        }

    }
}
