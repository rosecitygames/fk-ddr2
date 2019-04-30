using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.States;

namespace FloppyKnights.Agents
{
    public abstract class ScriptableBrain : ScriptableObject, IBrain
    {
        protected virtual IBrain GetBrain() { return null; }

        void IBrain.Init(AbstractCardAgent cardAgent) { }

        void IBrain.Destroy() { }

        void IStateTransitionHandler.HandleTransition(string transitionName) { }

        IBrain IBrain.Copy()
        {
            return GetBrain().Copy();
        }
    }
}
