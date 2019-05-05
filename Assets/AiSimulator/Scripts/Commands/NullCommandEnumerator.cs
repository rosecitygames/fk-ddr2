namespace RCG.Commands
{
    public class NullCommandEnumerator : ICommandEnumerator
    {
        ICommandEnumerator parent = null;
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

        int ICommandEnumerator.LoopCount { get => 0; set { } }
        int ICommandEnumerator.CurrentLoop { get => -1; }
        void ICommandEnumerator.HandleCompletedCommand(ICommand command) { }

        void ICommandCollection.AddCommand(ICommand command) { }
        void ICommandCollection.RemoveCommand(ICommand command) { }
        bool ICommandCollection.HasCommand(ICommand command) { return false; }

        public static ICommandEnumerator Create()
        {
            return new NullCommandEnumerator();
        }
    }
}