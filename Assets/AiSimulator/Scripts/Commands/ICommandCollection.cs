namespace IndieDevTools.Commands
{
    public interface ICommandCollection
    {
        void AddCommand(ICommand command);
        void RemoveCommand(ICommand command);
        bool HasCommand(ICommand command);
    }
}