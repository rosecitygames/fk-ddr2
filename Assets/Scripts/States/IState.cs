namespace RCG.States
{
    public interface IState
    {
        string StateName { get; }

        IStateMachine StateMachine { get; set; }

        void AddTransition(string transitionName, string toStateName);
        void RemoveTransition(string transitionName);
        void HandleTransition(string transitionName);

        void EnterState();
        void ExitState();

        void Destroy();
    }
}