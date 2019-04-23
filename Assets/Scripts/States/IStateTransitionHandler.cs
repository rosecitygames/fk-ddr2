using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.States
{
    public interface IStateTransitionHandler
    {
        void HandleTransition(string transitionName);
    }
}

