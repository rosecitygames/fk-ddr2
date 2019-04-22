using RCG.Attributes;

namespace RCG.Items
{ 
    public interface IItemData : IDescribable, IAttributeCollection
    {
        new IItemData Copy();
    }
}