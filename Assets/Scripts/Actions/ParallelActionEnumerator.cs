﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RCG.Actions
{
    public class ParallelActionEnumerator : AbstractActionEnumerator
    {
        override protected void OnStart()
        {
            currentLoop = 0;
            isCompleted = false;
            isStarted = true;
            actions.ForEach(StartAction);
        }
        protected void StartAction(IAction action)
        {
            action.Start();
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
            if (isCompleted == false)
            {
                int index = actions.IndexOf(action);
                if (index >= 0)
                {
                    bool isAllActionsCompleted = actions.TrueForAll(GetIsActionCompleted);
                    if (isAllActionsCompleted)
                    {
                        bool isLoopsRemaining = currentLoop < loopCount - 1;
                        bool isInfiniteLooping = loopCount < 0;
                        if (isLoopsRemaining || isInfiniteLooping)
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
        }

    }
}