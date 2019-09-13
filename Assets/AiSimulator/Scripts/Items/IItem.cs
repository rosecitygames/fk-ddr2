using IndieDevTools.Advertisements;
using IndieDevTools.Attributes;
using IndieDevTools.States;

namespace IndieDevTools.Items
{
    public interface IItem : IAdvertisingMapElement, IStateTransitionHandler
    {
        IItemData ItemData { get; set; }
    }
}