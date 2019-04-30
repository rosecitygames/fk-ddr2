using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Agents;

namespace RCG.Demo.BattleSimulator
{
    public interface IAttackReceiver
    {
        void ReceiveAttack(IAgent attackingAgent);
    }
}
