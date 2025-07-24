using System;
using System.Collections.Generic;
using TotalCommanderApp.Commands;

namespace TotalCommanderApp.Views
{
    public class HelpView
    {
        public void ShowCommandsHelp(IEnumerable<ICommand> commands)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=== Foydalaniladigan Komandalar ===");
            Console.WriteLine("Har bir komanda haqida to'liq ma'lumot olish uchun 'help <command>' ni kiriting\n");
            Console.ResetColor();

            foreach (var command in commands)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{command.Name.PadRight(15)}");
                Console.ResetColor();
                Console.WriteLine($"- {command.Description.Split('\n')[0]}");
            }
        }

        public void ShowCommandDetails(ICommand command)
        {
            if (command == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bunday komanda topilmadi!");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n=== {command.Name.ToUpper()} Komandasi ===");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nTavsif:");
            Console.ResetColor();
            Console.WriteLine(command.Description);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nNamuna:");
            Console.ResetColor();
            ShowCommandExample(command.Name);
        }

        private void ShowCommandExample(string commandName)
        {
            switch (commandName.ToLower())
            {
                case "copy":
                    Console.WriteLine("copy file.txt backup/file.txt");
                    Console.WriteLine("copy source_folder destination_folder");
                    break;
                case "delete":
                    Console.WriteLine("delete old_file.txt");
                    Console.WriteLine("delete empty_folder");
                    break;
                case "move":
                    Console.WriteLine("move file.txt new_location/");
                    Console.WriteLine("move folder new_parent_folder/");
                    break;
                case "rename":
                    Console.WriteLine("rename oldname.txt newname.txt");
                    Console.WriteLine("rename old_folder new_folder_name");
                    break;
                case "mkdir":
                    Console.WriteLine("mkdir new_folder");
                    Console.WriteLine("mkdir path/to/new/folder");
                    break;
                default:
                    Console.WriteLine("Komanda uchun namuna mavjud emas");
                    break;
            }
        }

        public void ShowWelcomeScreen()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"  _______           _        _____                                              
 |__   __|         | |      / ____|                                             
    | | ___   ___ | | __ _| |     ___  _ __ ___  _ __   ___  ___ __ _ _ __ 
    | |/ _ \ / _ \| |/ _` | |    / _ \| '_ ` _ \| '_ \ / _ \/ __/ _` | '__|
    | | (_) | (_) | | (_| | |___| (_) | | | | | | |_) |  __/ (_| (_| | |   
    |_|\___/ \___/|_|\__,_|\_____\___/|_| |_| |_| .__/ \___|\___\__,_|_|   
                                                | |                         
                                                |_|                         ");
            Console.ResetColor();
            Console.WriteLine("\nTotal Commander o'xshash konsol fayl menejeri. Version 1.0");
            Console.WriteLine("Yordam olish uchun 'help' yoki 'F1' tugmasini bosing.\n");
        }

        public void ShowShortcutsHelp()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=== Tezkor Tugmalar ===");
            Console.ResetColor();

            Console.WriteLine("F1  - Yordam");
            Console.WriteLine("F2  - Faylni ko'chirish");
            Console.WriteLine("F3  - Faylni ko'chirib o'tkazish");
            Console.WriteLine("F4  - Faylni o'chirish");
            Console.WriteLine("F5  - Faylni qayta nomlash");
            Console.WriteLine("F6  - Yangi papka yaratish");
            Console.WriteLine("F7  - Papkani almashtirish");
            Console.WriteLine("ESC - Dasturdan chiqish");
        }
    }
}