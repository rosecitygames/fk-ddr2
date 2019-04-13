namespace RCG.State
{
    abstract class AbstractState : IState
    {
        protected string stateName;
        public virtual string StateName
        {
            get { return stateName; }
        }

        protected IStateMachine stateMachine;
        public virtual IStateMachine StateMachine
        {
            get { return stateMachine; }
            set { stateMachine = value; }
        }

        public virtual void EnterState() { }

        public virtual void ExitState() { }
    }
}