using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;
using System;

namespace RCG.Demo.FloppyKnights.Cards
{
    [System.Serializable]
    public class AbstractCardAction : ICardAction
    {
        [SerializeField]
        string displayName = "";
        string IDescribable.DisplayName
        {
            get
            {
                return DisplayName;
            }
        }
        protected string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                displayName = value;
            }
        }

        [SerializeField]
        [TextArea]
        string description = "";
        string IDescribable.Description
        {
            get
            {
                return Description;
            }
        }
        protected string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        event Action<ICardAction> ICardAction.OnActionCompleted
        {
            add
            {
                OnActionCompleted += value;
            }
            remove
            {
                OnActionCompleted -= value;
            }
        }

        protected Action<ICardAction> OnActionCompleted;

        protected virtual void CallActionCompleted()
        {
            OnActionCompleted?.Invoke(this);
        }

        void ICardAction.StartAction(ICardPlayer cardPlayer)
        {
            StartAction(cardPlayer);
        }

        protected virtual void StartAction(ICardPlayer cardPlayer)
        {

        }

        ICardAction ICardAction.Copy()
        {
            return Copy();
        }

        protected virtual ICardAction Copy()
        {
            return this;
        }
    }
}
