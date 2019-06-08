using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloppyKnights.Cards
{
    public interface ICardActionCollection
    {
        List<ICardAction> CardActions { get; }
        bool HasCardAction(string cardActionId);
    }
}
