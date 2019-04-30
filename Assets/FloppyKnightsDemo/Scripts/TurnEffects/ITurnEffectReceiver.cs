using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Demo.FloppyKnights.TurnEffects
{
    public interface ITurnEffectCollector
    {
        void AddTurnEffect(ITurnEffect turnEffect);
        void RemoveTurnEffect(ITurnEffect turnEffect);
        void ClearTurnEffects();
    }
}