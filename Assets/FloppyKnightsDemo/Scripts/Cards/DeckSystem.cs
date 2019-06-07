using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloppyKnights.Cards
{
    public class DeckSystem : AbstractDeckSystem
    {
        public const string BaseDeckId = "base";
        public const string HandDeckId = "hand";
        public const string DiscardDeckId = "discard";

        public static IDeckSystem Create()
        {
            IDeckSystem deckSystem = new DeckSystem();
            deckSystem.AddDeck(BaseDeckId);
            deckSystem.AddDeck(HandDeckId);
            deckSystem.AddDeck(DiscardDeckId);
            return deckSystem;
        }
    }
}
