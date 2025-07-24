using System;
using System.IO;
using System.Threading;

namespace TotalCommanderApp.Utilities
{
    public sealed class Logger
    {
        private static readonly Lazy<Logger> _instance = 
            new Lazy<Logger>(() => new Logger(), LazyThreadSafetyMode.ExecutionAndPublication);

        private readonly string _logDirectory;
        private readonly string _logFilePath;
        private readonly object _lock = new object();

        public static Logger Instance => _instance.Value;

        private Logger()
        {
            _logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            Directory.CreateDirectory(_logDirectory);
            _logFilePath = Path.Combine(_logDirectory, $"tc_log_{DateTime.Now:yyyyMMdd}.txt");
        }

        public void LogInfo(string message)
        {
            Log("INFO", message);
        }

        public void LogWarning(string message)
        {
            Log("WARN", message);
        }

        public void LogError(string message)
        {
            Log("ERROR", message);
        }

        public void LogException(Exception ex)
        {
            Log("EXCEPTION", $"{ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}");
        }

        private void Log(string level, string message)
        {
            lock (_lock)
            {
                try
                {
                    string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{level}] {message}";
                    File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
                }
                catch
                {
                    // Logger o'zida xatolik bo'lsa, e'tiborsiz qoldiramiz
                }
            }
        }

        public static void CleanOldLogs(int daysToKeep = 7)
        {
            try
            {
                string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                if (!Directory.Exists(logDir))
                    return;

                var cutoffDate = DateTime.Now.AddDays(-daysToKeep);
                foreach (var file in Directory.GetFiles(logDir, "tc_log_*.txt"))
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.CreationTime < cutoffDate)
                    {
                        try
                        {
                            fileInfo.Delete();
                        }
                        catch
                        {
                            // O'chirishda xatolik bo'lsa, e'tiborsiz qoldiramiz
                        }
                    }
                }
            }
            catch
            {
                // Umumiy xatolik bo'lsa, e'tiborsiz qoldiramiz
            }
        }
    }
}