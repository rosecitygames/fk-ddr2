using RCG.States;

namespace FloppyKnights.Agents
{
    public interface IBrain : IStateTransitionHandler
    {
        void Init(ICardAgent cardAgent);
        void Destroy();
        IBrain Copy();
    }
}
