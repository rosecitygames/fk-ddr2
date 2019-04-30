using RCG.Agents;
using RCG.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FloppyKnights.Agents
{
    public interface IUnitAgent : IAgent, IAttackReceiver
    {
        event Action<IAgent> OnAttackReceived;
        event Action OnIdleStarted;

        void Move(Vector3Int location);
        void Attack(IUnitAgent targetAgent);
        void Buff(IAttributeCollection attributeCollection);
    }
}
