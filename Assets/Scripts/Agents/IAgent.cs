﻿using RCG.Advertisements;
using RCG.Attributes;
using RCG.Maps;
using RCG.States;
using System;

namespace RCG.Agents
{
    public interface IAgent : IMapElement, IDesiresCollection, IAdvertiser, IAdvertisementReceiver, IAdvertisementBroadcastData, IStateTransitionHandler
    {
        IAgentData AgentData { get; set; }
        event Action<IAdvertisement> OnAdvertisementReceived;
        IRankedAdvertisement TargetAdvertisement { get; set; }
    }
}

