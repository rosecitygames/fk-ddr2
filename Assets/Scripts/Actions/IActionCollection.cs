namespace RCG.Actions
{
    public interface IActionCollection
    {
        void AddAction(IAction action);
        void AddAction(IAction action, int layer);
        void RemoveAction(IAction action);
        void RemoveAction(IAction action, int layer);
        bool HasAction(IAction action);
    }
}