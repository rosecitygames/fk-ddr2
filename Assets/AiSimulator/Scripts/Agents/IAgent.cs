using IndieDevTools.Advertisements;
using IndieDevTools.Attributes;
using IndieDevTools.Maps;
using IndieDevTools.States;
using System;
using UnityEngine;

namespace IndieDevTools.Agents
{
    public interface IAgent : IAdvertisingMapElement, IAdvertisementReceiver, IStateTransitionHandler, IDesiresCollection
    {
        IAgentData Data { get; set; }

        event Action<IAdvertisement> OnAdvertisementReceived;

        IRankedAdvertisement TargetAdvertisement { get; set; }
        IMapElement TargetMapElement { get; set; }
        Vector2Int TargetLocation { get; set; }
    }
}

