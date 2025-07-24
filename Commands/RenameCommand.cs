using System;
using TotalCommanderApp.Models;
using TotalCommanderApp.Services;

namespace TotalCommanderApp.Commands
{
    public class RenameCommand : ICommand
    {
        private readonly FileManagerService _fileManager;

        public string Name => "rename";
        public string Description => "Fayl yoki papkani qayta nomlash\nUsage: rename <oldpath> <newname>";

        public RenameCommand(FileManagerService fileManager)
        {
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
        }

        public CommandResult Execute(string[] args)
        {
            if (args.Length < 2)
            {
                return CommandResult.ErrorResult("Iltimos, eski yo'l va yangi nomni kiriting");
            }

            string oldPath = args[0];
            string newName = args[1];

            return _fileManager.Rename(oldPath, newName);
        }
    }
}