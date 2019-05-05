using FloppyKnights.CardPlayers;
using RCG.Attributes;
using System;

namespace FloppyKnights.Cards
{
    public interface ICardAction : IDescribable
    {
        event Action<ICardAction> OnActionCompleted;
        void StartAction(ICardPlayer cardPlayer);
        ICardAction Copy();
    }
}
