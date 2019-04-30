using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloppyKnights.TurnEffects
{
    public interface ITurnEffectCollector
    {
        void AddTurnEffect(ITurnEffect turnEffect);
        void RemoveTurnEffect(ITurnEffect turnEffect);
        void ClearTurnEffects();
    }
}