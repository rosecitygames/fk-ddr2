using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Commands
{
    public class AbstractCommandEnumerator : AbstractCommand, ICommandEnumerator
    {
        protected List<ICommand> commands = new List<ICommand>();
        protected virtual int CommandsCount
        {
            get { return commands.Count; }
        }

        protected bool isActive = false;

        int ICommandEnumerator.LoopCount
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

        int ICommandEnumerator.CurrentLoop
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

        void ICommandCollection.AddCommand(ICommand command)
        {
            AddCommand(command);
        }
        protected virtual void AddCommand(ICommand command)
        {
            commands.Add(command);
            command.Parent = this;
        }

        void ICommandCollection.RemoveCommand(ICommand command)
        {
            RemoveCommand(command);
        }
        protected virtual void RemoveCommand(ICommand command)
        {
            command.Stop();
            commands.Remove(command);
        }

        bool ICommandCollection.HasCommand(ICommand command)
        {
            return HasCommand(command);
        }
        protected virtual bool HasCommand(ICommand command)
        {
            int commandIndex = commands.IndexOf(command);
            return commandIndex >= 0;
        }

        void ICommandEnumerator.HandleCompletedCommand(ICommand command)
        {
            HandleCompletedCommand(command);
        }
        protected virtual void HandleCompletedCommand(ICommand command) { }

        protected bool GetIsCommandCompleted(ICommand command)
        {
            return command.IsCompleted;
        }        
    }
}
