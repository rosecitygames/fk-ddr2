using System;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;

namespace RCG.Demo.FloppyKnights.Cards
{
    public interface ICardAction : IDescribable
    {
        event Action<ICardAction> OnActionCompleted;
        void StartAction(ICardPlayer cardPlayer);
        ICardAction Copy();
    }
}
