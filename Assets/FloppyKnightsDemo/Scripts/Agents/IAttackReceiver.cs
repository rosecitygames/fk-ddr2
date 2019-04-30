using RCG.Agents;

namespace FloppyKnights.Agents
{
    public interface IAttackReceiver
    {
        void ReceiveAttack(ICardAgent attackingAgent);
    }
}
