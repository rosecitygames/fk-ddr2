using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Agents;
using RCG.Attributes;
using System;

namespace RCG.Demo.FloppyKnights.Cards
{
    [CreateAssetMenu(fileName = "Move", menuName = "Floppy Knights/Actions/Move")]
    public class ScriptableMove : ScriptableCardAction
    {
        [SerializeField]
        Move cardAction = new Move();

        protected override ICardAction GetCardAction()
        {
            return cardAction as ICardAction;
        }
    }
}

