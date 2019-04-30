using RCG.Attributes;
using FloppyKnights.Cards;
using RCG.Maps;
using RCG.States;
using System;
using UnityEngine;
using FloppyKnights.TurnEffects;

namespace FloppyKnights.Agents
{
    public interface ICardAgent : IMapElement, IStateTransitionHandler, ITurnEffecter, ITurnEffectCollector
    {
        ICardData CardData { get; set; }

        IMapElement TargetMapElement { get; set; }
        Vector3Int TargetLocation { get; set; }

        event Action OnIdleStarted;

        void Move(Vector3Int location);
        void Attack(ICardAgent targetAgent);
        void Buff(IAttributeCollection attributeCollection);
    }
}
