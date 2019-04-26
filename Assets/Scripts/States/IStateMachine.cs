using System;

namespace RCG.States
{
    public interface IStateMachine : IStateTransitionHandler
    {
        event Action<string> OnStateChange;

        IState GetState(string stateName);
        void SetState(string stateName);
        void SetState(IState state);

        void AddState(IState state);
        void RemoveState(IState state);

        void Destroy();
    }
}