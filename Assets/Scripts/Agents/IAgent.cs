using RCG.Advertisements;
using RCG.Attributes;
using RCG.Maps;
using RCG.States;
using System;
using UnityEngine;

namespace RCG.Agents
{
    public interface IAgent : IMapElement, IDesiresCollection, IAdvertiser, IAdvertisementReceiver, IAdvertisementBroadcastData, IStateTransitionHandler
    {
        IAgentData AgentData { get; set; }

        event Action<IAdvertisement> OnAdvertisementReceived;

        IRankedAdvertisement TargetAdvertisement { get; set; }
        IAgent TargetAgent { get; set; }
        Vector3Int TargetLocation { get; set; }
    }
}

