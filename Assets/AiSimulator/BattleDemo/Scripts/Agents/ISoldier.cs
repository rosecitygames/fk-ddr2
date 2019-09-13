using IndieDevTools.Agents;
using System;

namespace IndieDevTools.Demo.BattleSimulator
{
    public interface ISoldier : IAgent, IAttackReceiver
    {
        event Action<IAgent> OnAttackReceived;
    }
}
