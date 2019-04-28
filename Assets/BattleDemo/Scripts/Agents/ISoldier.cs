using RCG.Agents;
using System;

namespace RCG.Demo.BattleSimulator
{
    public interface ISoldier : IAgent, IAttackReceiver
    {
        event Action<IAgent> OnAttackReceived;
    }
}
