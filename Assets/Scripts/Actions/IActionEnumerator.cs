namespace RCG.Actions
{
    public interface IActionEnumerator : IAction, IActionCollection
    {
        int LoopCount { get; set; }
        int CurrentLoop { get; }

        void HandleCompletedAction(IAction action);
    }
}
