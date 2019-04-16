namespace RCG.Actions
{
    public class NullActionEnumerator : IAction, IActionEnumerator
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

        void IActionEnumerator.AddAction(IAction action) { }
        void IActionEnumerator.AddAction(IAction action, int index) { }
        void IActionEnumerator.RemoveAction(IAction action) { }
        int IActionEnumerator.GetIndexOfAction(IAction action) { return -1; }
        void IActionEnumerator.HandleCompletedAction(IAction action) { }

        public static IActionEnumerator Create()
        {
            return new NullActionEnumerator();
        }
    }
}