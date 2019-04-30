using RCG.States;

namespace FloppyKnights.Agents
{
    public interface IBrain : IStateTransitionHandler
    {
        void Init(AbstractCardAgent cardAgent);
        void Destroy();
        IBrain Copy();
    }
}
