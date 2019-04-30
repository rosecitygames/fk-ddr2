using RCG.Attributes;

namespace RCG.Demo.FloppyKnights.Cards
{
    public interface ICardData : IDescribable, ICardActionCollection
    {
        ICardData Copy();
    }
}
