using System;
using System.IO;

namespace TotalCommanderApp.Models
{
    /// <summary>
    /// Fayl yoki papka haqida ma'lumotlarni saqlovchi model
    /// </summary>
    public class FileSystemItem
    {
        public string Name { get; }
        public string FullPath { get; }
        public string Type { get; }
        public long Size { get; }
        public DateTime ModifiedDate { get; }
        public bool IsDirectory { get; }
        public bool IsHidden { get; }
        public bool IsReadOnly { get; }

        /// <summary>
        /// FileSystemItem konstruktori
        /// </summary>
        /// <param name="path">Fayl yoki papka yo'li</param>
        public FileSystemItem(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Path cannot be null or empty");

            FullPath = Path.GetFullPath(path);
            Name = Path.GetFileName(path);

            if (Directory.Exists(path))
            {
                IsDirectory = true;
                Type = "Folder";
                Size = 0;
                var dirInfo = new DirectoryInfo(path);
                ModifiedDate = dirInfo.LastWriteTime;
                IsHidden = (dirInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                IsReadOnly = false; // Papkalar uchun read-only atributi yo'q
            }
            else if (File.Exists(path))
            {
                IsDirectory = false;
                Type = Path.GetExtension(path).ToUpper().TrimStart('.');
                var fileInfo = new FileInfo(path);
                Size = fileInfo.Length;
                ModifiedDate = fileInfo.LastWriteTime;
                IsHidden = (fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                IsReadOnly = (fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
            }
            else
            {
                throw new FileNotFoundException("Specified path does not exist", path);
            }
        }

        /// <summary>
        /// Fayl hajmini o'qiladigan formatda qaytaradi
        /// </summary>
        public string FormattedSize
        {
            get
            {
                if (IsDirectory) return string.Empty;
                
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                double len = Size;
                int order = 0;
                while (len >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    len /= 1024;
                }
                return $"{len:0.##} {sizes[order]}";
            }
        }

        /// <summary>
        /// Fayl yoki papka atributlarini belgilar ko'rinishida qaytaradi
        /// </summary>
        public string Attributes
        {
            get
            {
                return $"{(IsHidden ? "H" : "-")}{(IsReadOnly ? "R" : "-")}{(IsDirectory ? "D" : "-")}";
            }
        }
    }
}