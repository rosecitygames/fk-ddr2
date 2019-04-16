namespace RCG.Actions
{
    public interface IActionEnumerator : IAction
    {
        int LoopCount { get; set; }
        int CurrentLoop { get; }
        
        void AddAction(IAction action);
        void AddAction(IAction action, int index);
        void RemoveAction(IAction action);
        void HandleCompletedAction(IAction action);
        int GetIndexOfAction(IAction action);
    }
}
