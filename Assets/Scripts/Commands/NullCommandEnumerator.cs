namespace RCG.Commands
{
    public class NullCommandEnumerator : ICommandEnumerator
    {
        ICommandEnumerator parent = Create();
        ICommandEnumerator ICommand.Parent
        {
            get
            {
                return parent;
            }
            set { }
        }

        bool ICommand.IsCompleted
        {
            get
            {
                return true;
            }
        }

        void ICommand.Start() { }
        void ICommand.Stop() { }
        void ICommand.Destroy() { }

        int ICommandEnumerator.LoopCount { get { return 0; } set { } }
        int ICommandEnumerator.CurrentLoop { get { return -1; } }
        void ICommandEnumerator.HandleCompletedCommand(ICommand command) { }

        void ICommandCollection.AddCommand(ICommand command) { }
        void ICommandCollection.AddCommand(ICommand command, int layer) { }
        void ICommandCollection.RemoveCommand(ICommand command) { }
        void ICommandCollection.RemoveCommand(ICommand command, int layer) { }
        bool ICommandCollection.HasCommand(ICommand command) { return false; }

        public static ICommandEnumerator Create()
        {
            return new NullCommandEnumerator();
        }
    }
}