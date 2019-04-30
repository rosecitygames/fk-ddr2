using RCG.Attributes;
using RCG.Demo.FloppyKnights.Cards;
using RCG.Maps;
using RCG.States;
using System;
using UnityEngine;
using RCG.Demo.FloppyKnights.TurnEffects;

namespace RCG.Demo.FloppyKnights.Agents
{
    public interface ICardAgent : IMapElement, IStateTransitionHandler, ITurnEffecter, ITurnEffectCollector
    {

    }
}
