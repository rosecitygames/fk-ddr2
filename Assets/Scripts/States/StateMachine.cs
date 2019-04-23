using System;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.States
{
    public class StateMachine : IStateMachine
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

        public void SetState(IState state)
        {
            if (stateDictionary.ContainsValue(state))
            {
                SetCurrentState(state);
            }
        }

        public void SetState(string stateName)
        {
            if (stateDictionary.ContainsKey(stateName))
            {
                IState state = stateDictionary[stateName];
                SetCurrentState(state);
            }
        }

        void SetCurrentState(IState state)
        {
            if (currentState != state)
            {
                if (currentState != null)
                {
                    currentState.ExitState();
                }
                currentState = state;
                OnStateChange?.Invoke(state.StateName);
            }
            currentState.EnterState();
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