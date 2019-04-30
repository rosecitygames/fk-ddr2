using RCG.Advertisements;
using RCG.Attributes;
using RCG.Maps;
using RCG.States;
using System;
using UnityEngine;

namespace RCG.Agents
{
    public interface IAgent : IAdvertisingMapElement, IAdvertisementReceiver, IStateTransitionHandler, IDesiresCollection
    {
        IAgentData AgentData { get; set; }

        event Action<IAdvertisement> OnAdvertisementReceived;

        IRankedAdvertisement TargetAdvertisement { get; set; }
        IMapElement TargetMapElement { get; set; }
        Vector3Int TargetLocation { get; set; }
    }
}

