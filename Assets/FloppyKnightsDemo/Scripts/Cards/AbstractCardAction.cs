using FloppyKnights.CardPlayers;
using RCG.Attributes;
using System;
using UnityEngine;

namespace FloppyKnights.Cards
{
    [System.Serializable]
    public class AbstractCardAction : ICardAction
    {
        string IIdable.Id => Id;
        public string Id { get; set; }

        string IDescribable.DisplayName => DisplayName;
        protected string DisplayName { get => displayName; set => displayName = value; }
        [SerializeField]
        string displayName = "";
     
        string IDescribable.Description => Description;
        protected string Description { get => description; set => description = value; }
        [SerializeField]
        [TextArea]
        string description = "";

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
