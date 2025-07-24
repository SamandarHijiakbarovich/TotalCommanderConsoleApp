using System;
using System.Collections.Generic;
using System.IO;
using TotalCommanderApp.Models;
using TotalCommanderApp.Utilities;

namespace TotalCommanderApp.Services
{
    public class NavigationService
    {
        private string _currentDirectory;
        private readonly Stack<string> _directoryHistory = new Stack<string>();
        private readonly Logger _logger;

        public string CurrentDirectory
        {
            get => _currentDirectory;
            private set
            {
                _directoryHistory.Push(_currentDirectory);
                _currentDirectory = value;
            }
        }

        public NavigationService()
        {
            _currentDirectory = Directory.GetCurrentDirectory();
            _logger = Logger.Instance;
        }

        public List<FileSystemItem> GetDirectoryContents(string path = null)
        {
            try
            {
                string targetPath = path ?? CurrentDirectory;
                var contents = new List<FileSystemItem>();

                // Add parent directory link
                if (targetPath != Path.GetPathRoot(targetPath))
                {
                    contents.Add(new FileSystemItem(Path.Combine(targetPath, "..")));
                }

                // Get all directories
                foreach (var dir in Directory.GetDirectories(targetPath))
                {
                    contents.Add(new FileSystemItem(dir));
                }

                // Get all files
                foreach (var file in Directory.GetFiles(targetPath))
                {
                    contents.Add(new FileSystemItem(file));
                }

                return contents;
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetDirectoryContents failed: {ex.Message}");
                throw;
            }
        }

        public bool ChangeDirectory(string newPath)
        {
            try
            {
                if (newPath == "..")
                {
                    if (_directoryHistory.Count > 0)
                    {
                        CurrentDirectory = _directoryHistory.Pop();
                        return true;
                    }
                    return false;
                }

                string fullPath = Path.IsPathRooted(newPath) ? newPath : Path.Combine(CurrentDirectory, newPath);

                if (Directory.Exists(fullPath))
                {
                    CurrentDirectory = fullPath;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"ChangeDirectory failed: {ex.Message}");
                return false;
            }
        }

        public string[] GetAvailableDrives()
        {
            try
            {
                return Directory.GetLogicalDrives();
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAvailableDrives failed: {ex.Message}");
                return Array.Empty<string>();
            }
        }
    }
}