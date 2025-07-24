using System;
using System.Collections.Generic;
using TotalCommanderApp.Models;

namespace TotalCommanderApp.Views
{
    public class ConsoleRenderer
    {
        private const int NameWidth = 40;
        private const int TypeWidth = 10;
        private const int SizeWidth = 15;
        private const int DateWidth = 20;
        private const int AttributesWidth = 5;

        public void DisplayHeader(string currentPath)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('=', Console.WindowWidth - 1));
            Console.WriteLine($"Total Commander Console App - {currentPath}");
            Console.WriteLine(new string('=', Console.WindowWidth - 1));
            Console.ResetColor();
            Console.WriteLine();
        }

        public void DisplayFileList(List<FileSystemItem> items)
        {
            // Jadval sarlavhasi
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(
                $"{"Nomi".PadRight(NameWidth)} " +
                $"{"Turi".PadRight(TypeWidth)} " +
                $"{"Hajmi".PadRight(SizeWidth)} " +
                $"{"O'zgartirilgan".PadRight(DateWidth)} " +
                $"{"Attr".PadRight(AttributesWidth)}");
            Console.WriteLine(new string('-', Console.WindowWidth - 1));
            Console.ResetColor();

            // Fayl va papkalar ro'yxati
            foreach (var item in items)
            {
                if (item.Name == "..")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else if (item.IsDirectory)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.ForegroundColor = GetFileColor(item.Type);
                }

                Console.WriteLine(
                    $"{Truncate(item.Name, NameWidth).PadRight(NameWidth)} " +
                    $"{item.Type.PadRight(TypeWidth)} " +
                    $"{item.FormattedSize.PadRight(SizeWidth)} " +
                    $"{item.ModifiedDate.ToString("g").PadRight(DateWidth)} " +
                    $"{item.Attributes.PadRight(AttributesWidth)}");

                Console.ResetColor();
            }
        }

        public void DisplayStatusBar(string statusMessage = "")
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(new string('-', Console.WindowWidth - 1));
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("F1-Yordam | F2-Ko'chirish | F3-Ko'chirib o'tkazish | ");
            Console.WriteLine("F4-O'chirish | F5-Qayta nomlash | F6-Yangi papka | ESC-Chiqish");
            
            if (!string.IsNullOrEmpty(statusMessage))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(statusMessage);
            }
            
            Console.ResetColor();
        }

        public string GetUserInput(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(prompt);
            Console.ResetColor();
            return Console.ReadLine();
        }

        public void ShowMessage(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void ShowError(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ResetColor();
        }

        private ConsoleColor GetFileColor(string fileType)
        {
            return fileType.ToLower() switch
            {
                "exe" => ConsoleColor.Red,
                "txt" => ConsoleColor.White,
                "pdf" => ConsoleColor.Magenta,
                "jpg" or "png" or "gif" => ConsoleColor.DarkYellow,
                "zip" or "rar" => ConsoleColor.DarkMagenta,
                _ => ConsoleColor.Gray,
            };
        }

        private string Truncate(string value, int maxLength)
        {
            return value.Length <= maxLength ? value : value.Substring(0, maxLength - 3) + "...";
        }
    }
}