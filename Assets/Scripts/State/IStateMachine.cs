using System;

namespace RCG.State
{
    interface IStateMachine
    {
        event Action<string> OnStateChange;

        IState GetState(string stateName);
        void SetState(string stateName);

        void AddState(IState state);
        void RemoveState(IState state);
    }
}