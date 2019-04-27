using RCG.Agents;
using System;

namespace RCG.Demo.Simulator
{
    public interface ISoldier : IAgent, IAttackReceiver
    {
        event Action<IAgent> OnAttackReceived;
    }
}
