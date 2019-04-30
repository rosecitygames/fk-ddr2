using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Agents;
using RCG.Attributes;
using System;

namespace FloppyKnights.Cards
{
    [CreateAssetMenu(fileName = "Buff", menuName = "Floppy Knights/Actions/Buff")]
    public class ScriptableBuff : ScriptableCardAction
    {
        [SerializeField]
        Buff cardAction = new Buff();

        protected override ICardAction GetCardAction()
        {
            return cardAction as ICardAction;
        }
    }
}