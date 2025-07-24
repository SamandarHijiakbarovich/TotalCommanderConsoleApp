using System;
using System.Collections.Generic;
using TotalCommanderApp.Commands;
using TotalCommanderApp.Models;
using TotalCommanderApp.Services;
using TotalCommanderApp.Utilities;
using TotalCommanderApp.Views;


namespace TotalCommanderApp
{
    class Program
    {
        private static FileManagerService _fileManager;
        private static NavigationService _navigationService;
        private static CommandService _commandService;
        private static ConsoleRenderer _renderer;
        private static HelpView _helpView;

        static void Main(string[] args)
        {
            try
            {
                InitializeServices();
                RegisterCommands();
                Logger.CleanOldLogs();

                // Dastur boshlangich sozlamalari
                Console.Title = "Total Commander Console App";
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                // Boshlangich ekran
                _helpView.ShowWelcomeScreen();
                Console.WriteLine("Dastur ishga tushmoqda...");
                System.Threading.Thread.Sleep(1000);

                RunMainLoop();
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Kritik xato: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine("Dastur yopilmoqda...");
                Console.ReadKey();
            }
        }

        private static void InitializeServices()
        {
            _fileManager = new FileManagerService();
            _navigationService = new NavigationService();
            _commandService = new CommandService();
            _renderer = new ConsoleRenderer();
            _helpView = new HelpView();
        }

        private static void RegisterCommands()
        {
            var commands = new List<ICommand>
            {
                new CopyCommand(_fileManager),
                new DeleteCommand(_fileManager),
                new MoveCommand(_fileManager),
                new RenameCommand(_fileManager),
                new MakeDirCommand(_fileManager)
            };

            _commandService.RegisterCommands(commands);
        }

        private static void RunMainLoop()
        {
            bool isRunning = true;
            string statusMessage = "";

            while (isRunning)
            {
                try
                {
                    // Joriy papka tarkibini ko'rsatish
                    var currentItems = _navigationService.GetDirectoryContents();
                    _renderer.DisplayHeader(_navigationService.CurrentDirectory);
                    _renderer.DisplayFileList(currentItems);
                    _renderer.DisplayStatusBar(statusMessage);
                    statusMessage = ""; // Status xabarini tozalash

                    // Foydalanuvchi kiritishini olish
                    var key = Console.ReadKey(intercept: true);

                    // Tezkor tugmalarni tekshirish
                    switch (key.Key)
                    {
                        case ConsoleKey.F1:
                            ShowHelp();
                            break;
                        case ConsoleKey.Escape:
                            isRunning = false;
                            break;
                        case ConsoleKey.Enter:
                            statusMessage = ProcessCommandInput();
                            break;
                        default:
                            // Boshqa tugmalar uchun
                            break;
                    }
                }
                catch (Exception ex)
                {
                    statusMessage = $"Xato: {ex.Message}";
                    Logger.Instance.LogException(ex);
                }
            }

            Console.WriteLine("Dastur muvaffaqiyatli yakunlandi!");
        }

        private static string ProcessCommandInput()
        {
            Console.WriteLine();
            string input = _renderer.GetUserInput("Buyruq kiriting: ");
            if (string.IsNullOrWhiteSpace(input))
                return "Buyruq kiritilmadi";

            string[] parts = input.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            string commandName = parts[0].ToLower();
            string[] commandArgs = parts.Length > 1 ? 
                ParseArguments(parts[1]) : Array.Empty<string>();

            if (commandName == "help")
            {
                if (commandArgs.Length > 0)
                {
                    var command = _commandService.ExecuteCommand(commandArgs[0], Array.Empty<string>());
                    _helpView.ShowCommandDetails(command as ICommand);
                }
                else
                {
                    _helpView.ShowCommandsHelp(_commandService.GetAvailableCommands());
                }
                return "";
            }

            var result = _commandService.ExecuteCommand(commandName, commandArgs);
            return result.Message;
        }

        private static string[] ParseArguments(string argsString)
        {
            var argsList = new List<string>();
            bool inQuotes = false;
            int startIndex = 0;

            for (int i = 0; i < argsString.Length; i++)
            {
                if (argsString[i] == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (argsString[i] == ' ' && !inQuotes)
                {
                    if (i > startIndex)
                    {
                        string arg = argsString.Substring(startIndex, i - startIndex);
                        argsList.Add(arg.Trim('"'));
                    }
                    startIndex = i + 1;
                }
            }

            // Oxirgi argumentni qo'shish
            if (startIndex < argsString.Length)
            {
                string lastArg = argsString.Substring(startIndex).Trim();
                argsList.Add(lastArg.Trim('"'));
            }

            return argsList.ToArray();
        }

        private static void ShowHelp()
        {
            Console.Clear();
            _helpView.ShowShortcutsHelp();
            _helpView.ShowCommandsHelp(_commandService.GetAvailableCommands());
            Console.WriteLine("\nDavom etish uchun biror tugmani bosing...");
            Console.ReadKey();
        }
    }
}