using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RCG.States;

namespace FloppyKnights.Agents
{
    public class NullBrain : IBrain
    {
        void IBrain.Init(ICardAgent cardAgent) { }
        void IBrain.Destroy() { }
        IBrain IBrain.Copy() { return new NullBrain(); }
        void IStateTransitionHandler.HandleTransition(string transitionName) { }   
    }
}
