using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Agents;
using RCG.Attributes;
using System;

namespace FloppyKnights.Cards
{
    [CreateAssetMenu(fileName = "Attack", menuName = "Floppy Knights/Actions/Attack")]
    public class ScriptableAttack :ScriptableCardAction
    {
        [SerializeField]
        Attack cardAction = new Attack();

        protected override ICardAction GetCardAction()
        {
            return cardAction as ICardAction;
        }
    }
}

