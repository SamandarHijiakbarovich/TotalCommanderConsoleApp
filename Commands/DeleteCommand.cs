using System;
using TotalCommanderApp.Models;
using TotalCommanderApp.Services;

namespace TotalCommanderApp.Commands
{
    public class DeleteCommand : ICommand
    {
        private readonly FileManagerService _fileManager;

        public string Name => "delete";
        public string Description => "Fayl yoki papkani o'chirish\nUsage: delete <path>";

        public DeleteCommand(FileManagerService fileManager)
        {
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
        }

        public CommandResult Execute(string[] args)
        {
            if (args.Length < 1)
            {
                return CommandResult.ErrorResult("Iltimos, o'chiriladigan fayl yoki papka yo'lini kiriting");
            }

            string path = args[0];
            return _fileManager.Delete(path);
        }
    }
}