﻿using RCG.Agents;
using RCG.Attributes;
using UnityEngine;
using FloppyKnights.Agents;
using FloppyKnights.Cards;

namespace FloppyKnights.CardPlayers
{
    public interface ICardPlayer : IDescribable, IGroupMember, ITurnTaker, IDeckSystem
    {
        ICardAgent TargetAgent { get; }
        Vector3Int TargetLocation { get; }
    }
}
