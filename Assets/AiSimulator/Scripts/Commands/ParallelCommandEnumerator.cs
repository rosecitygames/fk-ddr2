using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IndieDevTools.Commands
{
    public class ParallelCommandEnumerator : AbstractCommandEnumerator
    {
        override protected void OnStart()
        {
            currentLoop = 0;
            isCompleted = false;
            isActive = true;
            commands.ForEach(StartCommand);
        }
        protected void StartCommand(ICommand command)
        {
            command.Start();
        }

        override protected void OnStop()
        {
            isActive = false;
            commands.ForEach(StopCommand);
            Complete();
        }
        protected void StopCommand(ICommand command)
        {
            command.Stop();
        }

        override protected void OnDestroy()
        {
            commands.ForEach(DestoryCommand);
            commands.Clear();
        }
        protected void DestoryCommand(ICommand command)
        {
            command.Destroy();
        }

        override protected void HandleCompletedCommand(ICommand command)
        {
            if (isCompleted == false)
            {
                int index = commands.IndexOf(command);
                if (index >= 0)
                {
                    bool isAllCommandsCompleted = commands.TrueForAll(GetIsCommandCompleted);
                    if (isAllCommandsCompleted)
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