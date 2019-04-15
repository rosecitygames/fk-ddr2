using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RCG.Actions
{
    public class ParallelActionEnumerator : AbstractAction, IActionEnumerator
    {


        protected List<IAction> actions = new List<IAction>();

        protected bool isStarted = false;

        public int LoopCount { get; protected set; }

        public int CurrentLoop { get; protected set; }

        public int ActionsCount
        {
            get { return actions.Count; }
        }

        public void AddAction(IAction action)
        {
            AddAction(action, -1);
        }
        public void AddAction(IAction action, int index)
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

        public void RemoveAction(IAction action)
        {
            actions.Remove(action);
        }

        public int IndexOfAction(IAction action)
        {
            return actions.IndexOf(action);
        }

        public void CompleteAction(IAction action)
        {
            if (IsCompleted == false)
            {
                int index = actions.IndexOf(action);
                if (index >= 0)
                {
                    bool isAllActionsComplete = actions.TrueForAll(CheckIsActionComplete);
                    if (isAllActionsComplete)
                    {
                        if (CurrentLoop < LoopCount - 1 || LoopCount < 0)
                        {
                            int nextLoop = CurrentLoop + 1;
                            Start();
                            CurrentLoop = nextLoop;
                        }
                        else
                        {
                            Complete();
                        }
                    }
                }
            }
        }
        protected bool CheckIsActionComplete(IAction action)
        {
            return action.IsCompleted;
        }

        override public void Start()
        {
            CurrentLoop = 0;
            IsCompleted = false;
            isStarted = true;
            actions.ForEach(StartAction);
        }
        protected void StartAction(IAction action)
        {
            action.Start();
        }

        override public void Stop()
        {
            isStarted = false;
            actions.ForEach(StopAction);
            Complete();
        }
        protected void StopAction(IAction action)
        {
            action.Stop();
        }

        override public void Destroy()
        {
            actions.ForEach(DestroyAction);
            actions.Clear();
        }
        protected void DestroyAction(IAction action)
        {
            action.Destroy();
        }

    }
}