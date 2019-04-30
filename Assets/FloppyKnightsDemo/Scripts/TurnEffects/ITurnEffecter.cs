using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloppyKnights.TurnEffects
{
    public interface ITurnEffecter
    {
        List<ITurnEffect> TurnEffects { get; }
    }
}
