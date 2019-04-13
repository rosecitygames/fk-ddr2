namespace RCG.State
{
    interface IState
    {
        string StateName { get; }

        IStateMachine StateMachine { get; set; }

        void EnterState();
        void ExitState();
    }
}