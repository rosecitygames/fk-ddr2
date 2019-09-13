namespace IndieDevTools.Commands
{
    public interface ICommandLayerCollection
    {
        void AddCommand(ICommand command, int layer);
        void RemoveCommand(ICommand command, int layer);
        int GetLayerLoopCount(int layer);
        void SetLayerLoopCount(int layer, int loopCount);
    }
}
