using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RCG.Actions
{
    public class SerialActionEnumerator : AbstractActionEnumerator
    {

        protected int currentIndex = 0;

        protected IAction CurrentAction
        {
            get { return actions[currentIndex]; }
        }

        override protected void OnStart()
        {
            currentIndex = 0;
            currentLoop = 0;
            isCompleted = false;
            isStarted = true;

            if (ActionsCount > 0)
            {
                CurrentAction.Start();
            }
        }

        override protected void OnStop()
        {
            isStarted = false;
            actions.ForEach(StopAction);
            Complete();
        }
        protected void StopAction(IAction action)
        {
            action.Stop();
        }

        override protected void OnDestroy()
        {
            actions.ForEach(DestroyAction);
            actions.Clear();
        }
        protected void DestroyAction(IAction action)
        {
            action.Destroy();
        }

        override protected void HandleCompletedAction(IAction action)
        {
            if (isCompleted == false && action == CurrentAction)
            {
                StartNextAction();
            }
        }

        protected void StartNextAction()
        {
            bool isActionsRemaining = currentIndex < ActionsCount - 1;
            bool isLoopsRemaining = currentLoop < loopCount;
            bool isInfiniteLooping = loopCount < 0;

            if (isActionsRemaining)
            {
                currentIndex += 1;
                CurrentAction.Start();
            }
            else if (isLoopsRemaining || isInfiniteLooping)
            {
                int nextLoop = currentLoop + 1;
                OnStart();
                currentLoop = nextLoop;
            }
            else
            {
                Complete();
            }
        }
    }
}