using System;
using System.Collections.Generic;

namespace RCG.States
{
    class StateMachine : IStateMachine
    {
        public event Action<string> OnStateChange;

        protected Dictionary<string, IState> stateDictionary = new Dictionary<string, IState>();

        protected IState currentState;
        public IState CurrentState { get { return currentState; } }

        public IState GetState(string stateType)
        {
            IState state = null;
            if (stateDictionary.ContainsKey(stateType))
            {
                state = stateDictionary[stateType];
            }
            return state;
        }

        public void SetState(string stateName)
        {
            if (stateDictionary.ContainsKey(stateName))
            {
                IState newState = stateDictionary[stateName];
                if (currentState != newState)
                {
                    if (currentState != null)
                    {
                        currentState.ExitState();
                    }
                    currentState = newState;

                    if (OnStateChange != null)
                    {
                        OnStateChange(stateName);
                    }
                }
                currentState.EnterState();
            }
        }

        public void AddState(IState state)
        {
            state.StateMachine = this;
            stateDictionary.Add(state.StateName, state);
        }

        public void RemoveState(IState state)
        {
            state.StateMachine = null;
            stateDictionary.Remove(state.StateName);
        }

        public static StateMachine Create()
        {
            return new StateMachine();
        }

    }
}