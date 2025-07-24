using TotalCommanderApp.Models;

namespace TotalCommanderApp.Commands
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        CommandResult Execute(string[] args);
    }
}