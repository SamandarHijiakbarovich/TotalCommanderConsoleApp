using System;

namespace TotalCommanderApp.Utilities
{
    public static class FileSizeFormatter
    {
        private static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public static string FormatSize(long bytes, int decimalPlaces = 2)
        {
            if (bytes < 0)
                return "-" + FormatSize(-bytes, decimalPlaces);

            if (bytes == 0)
                return "0 bytes";

            int mag = (int)Math.Log(bytes, 1024);
            decimal adjustedSize = (decimal)bytes / (1L << (mag * 10));

            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", 
                adjustedSize, 
                SizeSuffixes[mag]);
        }

        public static string FormatSize(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return "0 bytes";

            var fileInfo = new System.IO.FileInfo(filePath);
            return FormatSize(fileInfo.Length);
        }
    }
}