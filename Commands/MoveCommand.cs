using System;
using System.IO;
using TotalCommanderApp.Models;
using TotalCommanderApp.Services;

namespace TotalCommanderApp.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly FileManagerService _fileManager;

        public string Name => "move";
        public string Description => "Fayl yoki papkani ko'chirib o'tkazish\nUsage: move <source> <destination>";

        public MoveCommand(FileManagerService fileManager)
        {
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
        }

        public CommandResult Execute(string[] args)
        {
            if (args.Length < 2)
            {
                return CommandResult.ErrorResult("Iltimos, manba va yangi joylashuvni kiriting");
            }

            string source = args[0];
            string destination = args[1];

            // Avval nusxalaymiz
            var copyResult = _fileManager.Copy(source, destination);
            if (!copyResult.Success)
            {
                return copyResult;
            }

            // Keyin aslini o'chiramiz
            var deleteResult = _fileManager.Delete(source);
            if (!deleteResult.Success)
            {
                // Agar o'chirishda xatolik bo'lsa, nusxani ham o'chiramiz
                _fileManager.Delete(destination);
                return deleteResult;
            }

            return CommandResult.SuccessResult($"'{source}' muvaffaqiyatli ko'chirib o'tkazildi");
        }
    }
}