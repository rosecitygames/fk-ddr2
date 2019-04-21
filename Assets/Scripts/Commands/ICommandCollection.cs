namespace RCG.Commands
{
    public interface ICommandCollection
    {
        void AddCommand(ICommand command);
        void AddCommand(ICommand command, int layer);
        void RemoveCommand(ICommand command);
        void RemoveCommand(ICommand command, int layer);
        bool HasCommand(ICommand command);
    }
}