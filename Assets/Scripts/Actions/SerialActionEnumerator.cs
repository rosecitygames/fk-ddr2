using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RCG.Actions
{
    public class SerialActionEnumerator : AbstractAction, IActionEnumerator
    {


        protected List<IAction> actions = new List<IAction>();

        protected bool isStarted = false;

        public int LoopCount { get; set; }

        public int CurrentLoop { get; protected set; }

        public int ActionsCount
        {
            get { return actions.Count; }
        }

        protected int currentIndex = 0;

        protected IAction CurrentAction
        {
            get { return actions[currentIndex]; }
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

        override public void Start()
        {
            currentIndex = 0;
            CurrentLoop = 0;
            IsCompleted = false;
            isStarted = true;
            if (ActionsCount > 0)
            {
                CurrentAction.Start();
            }
        }

        override public void Stop()
        {
            isStarted = false;
            if (ActionsCount > 0)
            {
                CurrentAction.Stop();
            }
            Complete();
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

        public void CompleteAction(IAction action)
        {
            if (IsCompleted == false && action == CurrentAction)
            {
                StartNextAction();
            }
        }

        protected void StartNextAction()
        {
            if (currentIndex < ActionsCount - 1)
            {
                currentIndex += 1;
                CurrentAction.Start();
            }
            else if (CurrentLoop < LoopCount || LoopCount < 0)
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