using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Agents;
using RCG.Attributes;
using System;

namespace FloppyKnights.Cards
{
    [CreateAssetMenu(fileName = "AddCard", menuName = "Floppy Knights/Actions/Add Card")]
    public class ScriptableAddCard : ScriptableCardAction
    {
        [SerializeField]
        AddCard cardAction = new AddCard();

        protected override ICardAction GetCardAction()
        {
            return cardAction as ICardAction;
        }
    }
}
