using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FloppyKnights.Cards;

namespace FloppyKnights.TurnEffects
{
    public interface ITurnEffect
    {
        ICardAction CardData { get; }
        int Range { get; }
        int Lifetime { get; }
    }
}
