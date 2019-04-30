using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.States;

namespace FloppyKnights.Agents
{
    public class AbstractBrain : IBrain
    {
        void IBrain.Init(ICardAgent cardAgent)
        {
            CardAgent = cardAgent;
            InitStateMachine();
        }

        void IBrain.Destroy()
        {
            Destroy();
        }

        protected void Destroy()
        {
            if (stateMachine != null)
            {
                stateMachine.Destroy();
            }     
        }

        IBrain IBrain.Copy()
        {
            return Copy();
        }

        protected virtual IBrain Copy()
        {
            return new NullBrain();
        }

        protected ICardAgent CardAgent { get; set; }

        protected IStateMachine stateMachine = StateMachine.Create();

        protected virtual void InitStateMachine() { }

        void IStateTransitionHandler.HandleTransition(string transitionName)
        {
            HandleTransition(transitionName);
        }
        protected virtual void HandleTransition(string transitionName)
        {
            stateMachine.HandleTransition(transitionName);
        }
    }
}
