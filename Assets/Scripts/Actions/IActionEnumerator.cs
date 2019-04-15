namespace RCG.Actions
{
    public interface IActionEnumerator
    {

        void AddAction(IAction action);
        void AddAction(IAction action, int index);
        void RemoveAction(IAction action);

        void CompleteAction(IAction action);

    }
}
