namespace RCG.Actions
{
    abstract public class NullAction : IAction
    {
        bool IAction.IsCompleted
        {
            get
            {
                return true;
            }
        }

        protected IActionEnumerator parent = NullActionEnumerator.Create();
        IActionEnumerator IAction.Parent
        {
            get
            {
                return parent;
            }
            set { }
        }

        void IAction.Start() { }
        void IAction.Stop() { }
        void IAction.Destroy() { }
    }
}