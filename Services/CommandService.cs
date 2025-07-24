using System;
using System.Collections.Generic;
using System.Linq;
using TotalCommanderApp.Commands;
using TotalCommanderApp.Models;
using TotalCommanderApp.Utilities;

namespace TotalCommanderApp.Services
{
    public class CommandService
    {
        private readonly Dictionary<string, ICommand> _commands;
        private readonly Logger _logger;

        public CommandService()
        {
            _logger = Logger.Instance;
            _commands = new Dictionary<string, ICommand>(StringComparer.OrdinalIgnoreCase);
        }

        public void RegisterCommand(ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            _commands[command.Name] = command;
        }

        public void RegisterCommands(IEnumerable<ICommand> commands)
        {
            foreach (var command in commands)
            {
                RegisterCommand(command);
            }
        }

        public CommandResult ExecuteCommand(string commandName, string[] args)
        {
            try
            {
                if (_commands.TryGetValue(commandName, out var command))
                {
                    return command.Execute(args);
                }
                return CommandResult.ErrorResult($"Unknown command: {commandName}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"ExecuteCommand failed: {ex.Message}");
                return CommandResult.FromException(ex);
            }
        }

       public IEnumerable<ICommand> GetAvailableCommands()
{
    return _commands.Values.OrderBy(c => c.Name);
}

        public string GetCommandDescription(string commandName)
        {
            if (_commands.TryGetValue(commandName, out var command))
            {
                return command.Description;
            }
            return "Command not found";
        }
    }
}