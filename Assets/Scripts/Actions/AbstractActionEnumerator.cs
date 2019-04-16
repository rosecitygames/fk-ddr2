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

        void IActionEnumerator.AddAction(IAction action)
        {
            AddAction(action);
        }
        protected virtual void AddAction(IAction action)
        {
            AddAction(action, -1);
        }

        void IActionEnumerator.AddAction(IAction action, int index)
        {
            AddAction(action, index);
        }
        protected virtual void AddAction(IAction action, int index)
        {
            if (index < 0)
            {
                actions.Add(action);
            }
            else
            {
                actions.Insert(index, action);
            }
            action.Parent = this;
        }

        void IActionEnumerator.RemoveAction(IAction action)
        {
            RemoveAction(action);
        }
        protected virtual void RemoveAction(IAction action)
        {
            action.Stop();
            actions.Remove(action);
        }

        bool IActionEnumerator.HasAction(IAction action)
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
