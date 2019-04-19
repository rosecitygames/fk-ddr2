namespace RCG.Actions
{
    public class NullActionEnumerator : IActionEnumerator
    {
        IActionEnumerator parent = Create();
        IActionEnumerator IAction.Parent
        {
            get
            {
                return parent;
            }
            set { }
        }

        bool IAction.IsCompleted
        {
            get
            {
                return true;
            }
        }

        void IAction.Start() { }
        void IAction.Stop() { }
        void IAction.Destroy() { }

        int IActionEnumerator.LoopCount { get { return 0; } set { } }
        int IActionEnumerator.CurrentLoop { get { return -1; } }
        void IActionEnumerator.HandleCompletedAction(IAction action) { }

        void IActionCollection.AddAction(IAction action) { }
        void IActionCollection.AddAction(IAction action, int layer) { }
        void IActionCollection.RemoveAction(IAction action) { }
        void IActionCollection.RemoveAction(IAction action, int layer) { }
        bool IActionCollection.HasAction(IAction action) { return false; }

        public static IActionEnumerator Create()
        {
            return new NullActionEnumerator();
        }
    }
}