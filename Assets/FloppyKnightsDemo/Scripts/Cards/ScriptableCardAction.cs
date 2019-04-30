using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Agents;
using RCG.Attributes;
using System;

namespace FloppyKnights.Cards
{
    public abstract class ScriptableCardAction : ScriptableObject, ICardAction
    {
        protected virtual ICardAction GetCardAction() { return null; }

        string IDescribable.DisplayName { get { return GetCardAction().DisplayName; } }
        string IDescribable.Description { get { return GetCardAction().Description; } }

        event Action<ICardAction> ICardAction.OnActionCompleted { add { } remove { } }

        void ICardAction.StartAction(ICardPlayer cardPlayer) { }

        ICardAction ICardAction.Copy()
        {
            return GetCardAction().Copy();
        }
    }
}

