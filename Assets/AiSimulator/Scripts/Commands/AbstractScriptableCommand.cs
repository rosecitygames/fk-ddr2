using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Commands
{
    abstract public class AbstractScriptableCommand : ScriptableObject, ICommand
    {
        protected bool isCompleted;
        bool ICommand.IsCompleted
        {
            get
            {
                return isCompleted;
            }
        }

        protected ICommandEnumerator parent = NullCommandEnumerator.Create();
        ICommandEnumerator ICommand.Parent
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

        void ICommand.Start()
        {
            isCompleted = false;
            OnStart();
        }

        void ICommand.Stop()
        {
            OnStop();
            Complete();
        }

        void ICommand.Destroy()
        {
            OnDestroy();
        }

        protected virtual void Complete()
        {
            isCompleted = true;
            parent.HandleCompletedCommand(this);
        }

        protected virtual void OnStart() { }

        protected virtual void OnStop() { }

        protected virtual void OnDestroy() { }

    }
}