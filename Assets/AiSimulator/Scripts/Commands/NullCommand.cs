namespace RCG.Commands
{
    public class NullCommand : ICommand
    {
        bool ICommand.IsCompleted
        {
            get
            {
                return true;
            }
        }

        protected ICommandEnumerator parent = NullCommandEnumerator.Create();
        ICommandEnumerator ICommand.Parent
        {
            get
            {
                return parent;
            }
            set { }
        }

        void ICommand.Start() { }
        void ICommand.Stop() { }
        void ICommand.Destroy() { }

        public static ICommand Create() => new NullCommand();
    }
}