using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TotalCommanderApp.Utilities
{
    public static class PathValidator
    {
        private static readonly Regex InvalidCharsRegex = 
            new Regex($"[{Regex.Escape(new string(Path.GetInvalidPathChars()))}]");

        private static readonly Regex InvalidFileNameCharsRegex = 
            new Regex($"[{Regex.Escape(new string(Path.GetInvalidFileNameChars()))}]");

        public static bool IsValidPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            // Umumiy path tekshiruvi
            if (InvalidCharsRegex.IsMatch(path))
                return false;

            try
            {
                // Absolyut path uchun qo'shimcha tekshiruv
                if (Path.IsPathRooted(path))
                {
                    string root = Path.GetPathRoot(path);
                    if (!Directory.Exists(root))
                        return false;

                    // Diskda joy borligini tekshirish
                    var drives = DriveInfo.GetDrives();
                    bool driveExists = false;
                    foreach (var drive in drives)
                    {
                        if (string.Equals(drive.Name, root, StringComparison.OrdinalIgnoreCase))
                        {
                            driveExists = true;
                            break;
                        }
                    }
                    if (!driveExists)
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            if (fileName.Length > 260) // MAX_PATH
                return false;

            return !InvalidFileNameCharsRegex.IsMatch(fileName);
        }

        public static string SanitizePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return string.Empty;

            char[] invalidChars = Path.GetInvalidPathChars();
            foreach (char c in invalidChars)
            {
                path = path.Replace(c.ToString(), "");
            }

            return path.Trim();
        }

        public static string SanitizeFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return string.Empty;

            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char c in invalidChars)
            {
                fileName = fileName.Replace(c.ToString(), "");
            }

            return fileName.Trim();
        }
    }
}