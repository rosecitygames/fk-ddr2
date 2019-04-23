using System.Collections.Generic;

namespace RCG.States
{
    public abstract class AbstractState : IState
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

        protected Dictionary<string, string> transitionNamesToStateNames = new Dictionary<string, string>();

        public virtual void AddTransition(string transitionName, string toStateName)
        {
            if (string.IsNullOrEmpty(transitionName) || string.IsNullOrEmpty(toStateName)) return;

            bool containsKey = transitionNamesToStateNames.ContainsKey(transitionName);
            if (containsKey)
            {
                transitionNamesToStateNames[transitionName] = toStateName;
            }
            else
            {
                transitionNamesToStateNames.Add(transitionName, toStateName);
            }
        }

        public virtual void RemoveTransition(string transitionName)
        {
            if (string.IsNullOrEmpty(transitionName)) return;

            bool containsKey = transitionNamesToStateNames.ContainsKey(transitionName);
            if (containsKey)
            {
                transitionNamesToStateNames.Remove(transitionName);
            }
        }

        public virtual void HandleTransition(string transitionName)
        {
            if (string.IsNullOrWhiteSpace(transitionName)) return;

            if (stateMachine == null) return;

            bool containsKey = transitionNamesToStateNames.ContainsKey(transitionName);
            if (containsKey)
            {
                string toStateName = transitionNamesToStateNames[transitionName];
                stateMachine.SetState(toStateName);
            }
        }

        public virtual void EnterState() { }
        public virtual void ExitState() { }

        public virtual void Destroy() { }

    }
}