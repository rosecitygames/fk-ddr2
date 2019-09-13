using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieDevTools.Agents;

namespace IndieDevTools.Demo.BattleSimulator
{
    public interface IAttackReceiver
    {
        void ReceiveAttack(IAgent attackingAgent);
    }
}
