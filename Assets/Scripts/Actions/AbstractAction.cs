using System.Collections;
using System.Collections.Generic;

namespace RCG.Actions
{
    abstract public class AbstractAction : IAction
    {
        public virtual bool IsCompleted { get; protected set; }

        public virtual IActionEnumerator Parent { get; set; }

        public virtual void Start()
        {
            IsCompleted = false;
            OnStart();
        }

        public virtual void Stop()
        {
            OnStop();
            Complete();
        }

        public virtual void Destroy()
        {
            OnDestroy();
        }

        protected virtual void Complete()
        {
            IsCompleted = true;
            if (Parent != null)
            {
                Parent.CompleteAction(this);
            }
        }

        protected virtual void OnStart() { }

        protected virtual void OnStop() { }

        protected virtual void OnDestroy() { }

    }
}