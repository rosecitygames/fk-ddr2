using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.States
{
    public interface IStateTransitionHandler
    {
        void HandleTransition(string transitionName);
    }
}

