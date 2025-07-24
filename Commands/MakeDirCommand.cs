using System;
using TotalCommanderApp.Models;
using TotalCommanderApp.Services;

namespace TotalCommanderApp.Commands
{
    public class MakeDirCommand : ICommand
    {
        private readonly FileManagerService _fileManager;

        public string Name => "mkdir";
        public string Description => "Yangi papka yaratish\nUsage: mkdir <dirname>";

        public MakeDirCommand(FileManagerService fileManager)
        {
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
        }

        public CommandResult Execute(string[] args)
        {
            if (args.Length < 1)
            {
                return CommandResult.ErrorResult("Iltimos, yangi papka nomini kiriting");
            }

            string dirName = args[0];
            return _fileManager.CreateDirectory(dirName);
        }
    }
}