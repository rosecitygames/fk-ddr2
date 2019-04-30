using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Demo.FloppyKnights.Cards;

namespace RCG.Demo.FloppyKnights.TurnEffects
{
    public interface ITurnEffect
    {
        ICardAction CardData { get; }
        int Range { get; }
        int Lifetime { get; }
    }
}
