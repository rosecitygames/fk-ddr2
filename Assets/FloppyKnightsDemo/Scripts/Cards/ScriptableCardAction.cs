using FloppyKnights.CardPlayers;
using RCG.Attributes;
using System;
using UnityEngine;

namespace FloppyKnights.Cards
{
    public abstract class ScriptableCardAction : ScriptableObject, ICardAction
    {
        protected virtual ICardAction GetCardAction() { return null; }

        string IIdable.Id => GetCardAction().Id;

        string IDescribable.DisplayName => GetCardAction().DisplayName;
        string IDescribable.Description => GetCardAction().Description;

        event Action<ICardAction> ICardAction.OnActionCompleted { add { } remove { } }

        void ICardAction.StartAction(ICardPlayer cardPlayer) { }

        ICardAction ICardAction.Copy()
        {
            return GetCardAction().Copy();
        }
    }
}

