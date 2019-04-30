using RCG.Attributes;

namespace FloppyKnights.Cards
{
    public interface ICardData : IDescribable, IStatsCollection, ICardActionCollection
    {
        ICardData Copy();
    }
}
