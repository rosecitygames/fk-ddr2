using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Demo.FloppyKnights.TurnEffects
{
    public interface ITurnEffecter
    {
        List<ITurnEffect> TurnEffects { get; }
    }
}
