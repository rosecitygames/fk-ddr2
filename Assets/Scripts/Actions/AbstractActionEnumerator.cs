using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Actions
{
    public class AbstractActionEnumerator : AbstractAction, IActionEnumerator
    {
        protected List<IAction> actions = new List<IAction>();
        protected virtual int ActionsCount
        {
            get { return actions.Count; }
        }

        protected bool isStarted = false;

        int IActionEnumerator.LoopCount
        {
            get
            {
                return LoopCount;
            }
            set
            {
                LoopCount = value;
            }
        }
        protected virtual int LoopCount
        {
            get
            {
                return loopCount;
            }
            set
            {
                loopCount = value;
            }
        }
        protected int loopCount = 0;

        int IActionEnumerator.CurrentLoop
        {
            get
            {
                return currentLoop;
            }
        }
        protected virtual int CurrentLoop
        {
            get
            {
                return currentLoop;
            }
        }
        protected int currentLoop;

        void IActionCollection.AddAction(IAction action)
        {
            AddAction(action);
        }
        void IActionCollection.AddAction(IAction action, int layer)
        {
            AddAction(action);
        }
        protected virtual void AddAction(IAction action)
        {
            actions.Add(action);
            action.Parent = this;
        }

        void IActionCollection.RemoveAction(IAction action)
        {
            RemoveAction(action);
        }
        void IActionCollection.RemoveAction(IAction action, int layer)
        {
            RemoveAction(action);
        }
        protected virtual void RemoveAction(IAction action)
        {
            action.Stop();
            actions.Remove(action);
        }

        bool IActionCollection.HasAction(IAction action)
        {
            return HasAction(action);
        }
        protected virtual bool HasAction(IAction action)
        {
            int actionIndex = actions.IndexOf(action);
            return actionIndex >= 0;
        }

        void IActionEnumerator.HandleCompletedAction(IAction action)
        {
            HandleCompletedAction(action);
        }
        protected virtual void HandleCompletedAction(IAction action) { }

        protected bool GetIsActionCompleted(IAction action)
        {
            return action.IsCompleted;
        }
        
    }
}
