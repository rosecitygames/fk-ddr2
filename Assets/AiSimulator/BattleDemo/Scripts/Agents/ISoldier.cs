using IndieDevTools.Agents;

namespace IndieDevTools.Demo.BattleSimulator
{
    /// <summary>
    /// Interface for an agent that can receive attacks and invoke an event that the attack was received.
    /// </summary>
    public interface ISoldier : IAgent, IAttackReceiver { }
}
