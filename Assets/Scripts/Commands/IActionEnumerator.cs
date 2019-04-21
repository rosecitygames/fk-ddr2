namespace RCG.Commands
{
    public interface ICommandEnumerator : ICommand, ICommandCollection
    {
        int LoopCount { get; set; }
        int CurrentLoop { get; }

        void HandleCompletedCommand(ICommand command);
    }
}
