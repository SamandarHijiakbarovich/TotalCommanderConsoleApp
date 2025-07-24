using System;
using System.IO;
using TotalCommanderApp.Models;
using TotalCommanderApp.Utilities;

namespace TotalCommanderApp.Services
{
    public class FileManagerService
    {
        private readonly Logger _logger;

        public FileManagerService()
        {
            _logger = Logger.Instance;
        }

        public CommandResult Copy(string sourcePath, string destinationPath)
        {
            try
            {
                if (Directory.Exists(sourcePath))
                {
                    Directory.CreateDirectory(destinationPath);
                    foreach (var file in Directory.GetFiles(sourcePath))
                    {
                        string fileName = Path.GetFileName(file);
                        string destFile = Path.Combine(destinationPath, fileName);
                        File.Copy(file, destFile, true);
                    }
                    foreach (var dir in Directory.GetDirectories(sourcePath))
                    {
                        string dirName = Path.GetFileName(dir);
                        string destDir = Path.Combine(destinationPath, dirName);
                        Copy(dir, destDir);
                    }
                    return CommandResult.SuccessResult($"Successfully copied '{sourcePath}' to '{destinationPath}'");
                }
                else if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, destinationPath, true);
                    return CommandResult.SuccessResult($"Successfully copied file to '{destinationPath}'");
                }
                return CommandResult.ErrorResult($"Source path not found: {sourcePath}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Copy failed: {ex.Message}");
                return CommandResult.FromException(ex);
            }
        }

        public CommandResult Delete(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    return CommandResult.SuccessResult($"Directory '{path}' deleted successfully");
                }
                else if (File.Exists(path))
                {
                    File.Delete(path);
                    return CommandResult.SuccessResult($"File '{path}' deleted successfully");
                }
                return CommandResult.ErrorResult($"Path not found: {path}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete failed: {ex.Message}");
                return CommandResult.FromException(ex);
            }
        }

        public CommandResult Rename(string oldPath, string newName)
        {
            try
            {
                string newPath = Path.Combine(Path.GetDirectoryName(oldPath), newName);
                
                if (Directory.Exists(oldPath))
                {
                    Directory.Move(oldPath, newPath);
                    return CommandResult.SuccessResult($"Directory renamed to '{newName}'");
                }
                else if (File.Exists(oldPath))
                {
                    File.Move(oldPath, newPath);
                    return CommandResult.SuccessResult($"File renamed to '{newName}'");
                }
                return CommandResult.ErrorResult($"Path not found: {oldPath}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Rename failed: {ex.Message}");
                return CommandResult.FromException(ex);
            }
        }

        public CommandResult CreateDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
                return CommandResult.SuccessResult($"Directory '{path}' created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateDirectory failed: {ex.Message}");
                return CommandResult.FromException(ex);
            }
        }
    }
}