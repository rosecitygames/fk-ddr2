using IndieDevTools.Advertisements;
using IndieDevTools.Traits;
using IndieDevTools.States;

namespace IndieDevTools.Items
{
    public interface IItem : IAdvertisingMapElement, IStateTransitionHandler
    {
        IItemData Data { get; set; }
    }
}