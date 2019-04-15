using System;

namespace RCG.States
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