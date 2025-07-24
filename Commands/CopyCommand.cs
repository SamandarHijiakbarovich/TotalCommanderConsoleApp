using System;
using TotalCommanderApp.Models;
using TotalCommanderApp.Services;

namespace TotalCommanderApp.Commands
{
    public class CopyCommand : ICommand
    {
        private readonly FileManagerService _fileManager;

        public string Name => "copy";
        public string Description => "Fayl yoki papkani nusxalash\nUsage: copy <source> <destination>";

        public CopyCommand(FileManagerService fileManager)
        {
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
        }

        public CommandResult Execute(string[] args)
        {
            if (args.Length < 2)
            {
                return CommandResult.ErrorResult("Iltimos, manba va nusha joyini kiriting");
            }

            string source = args[0];
            string destination = args[1];

            return _fileManager.Copy(source, destination);
        }
    }
}