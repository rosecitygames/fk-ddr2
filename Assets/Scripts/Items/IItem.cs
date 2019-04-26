using RCG.Advertisements;
using RCG.Attributes;
using RCG.States;

namespace RCG.Items
{
    public interface IItem : IAdvertisingMapElement, IStateTransitionHandler
    {
        IItemData ItemData { get; set; }
    }
}