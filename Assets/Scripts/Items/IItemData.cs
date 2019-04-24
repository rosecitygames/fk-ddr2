using RCG.Attributes;

namespace RCG.Items
{ 
    public interface IItemData : IDescribable, IStatsCollection
    {
        IItemData Copy();
    }
}