using System.Collections;
using System.Collections.Generic;

namespace RCG.Actions
{
    abstract public class AbstractAction : IAction
    {
        protected bool isCompleted;
        bool IAction.IsCompleted
        {
            get
            {
                return isCompleted;
            }
        }

        protected IActionEnumerator parent = NullActionEnumerator.Create();
        IActionEnumerator IAction.Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        void IAction.Start()
        {
            isCompleted = false;
            OnStart();
        }

        void IAction.Stop()
        {
            OnStop();
            Complete();
        }

        void IAction.Destroy()
        {
            OnDestroy();
        }

        protected virtual void Complete()
        {
            isCompleted = true;
            parent.HandleCompletedAction(this);
        }

        protected virtual void OnStart() { }

        protected virtual void OnStop() { }

        protected virtual void OnDestroy() { }

    }
}