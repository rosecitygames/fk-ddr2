using RCG.Attributes;

namespace FloppyKnights.Cards
{
    public interface ICardData : IDescribable, ICardActionCollection
    {
        ICardData Copy();
    }
}
