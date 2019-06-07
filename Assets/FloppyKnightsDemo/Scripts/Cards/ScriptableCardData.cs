using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;
using FloppyKnights.Agents;

namespace FloppyKnights.Cards
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Floppy Knights/Card Data")]
    public class ScriptableCardData : ScriptableObject, ICardData
    {
        [SerializeField]
        string displayName = "";
        string IDescribable.DisplayName => displayName;

        [SerializeField]
        [TextArea]
        string description = "";
        string IDescribable.Description => description;

        [SerializeField]
        Sprite agentSprite = null;
        Sprite ICardData.AgentSprite => agentSprite;

        [SerializeField]
        AttributeCollection stats = new AttributeCollection();
        IAttributeCollection Stats => stats as IAttributeCollection;
        List<IAttribute> IStatsCollection.Stats => Stats.Attributes;

        IAttribute IStatsCollection.GetStat(string id) => Stats.GetAttribute(id);
        
        [SerializeField]
        List<ScriptableCardAction> cardActions = new List<ScriptableCardAction>();

        List<ICardAction> ICardActionCollection.CardActions
        {
            get
            {
                List<ICardAction> cardActionsCopy = new List<ICardAction>();
                foreach (ICardAction cardAction in cardActions)
                {
                    cardActionsCopy.Add(cardAction.Copy());
                }
                return cardActionsCopy;
            }
        }

        ICardData ICardData.Copy()
        {
            return CardData.Create(this);
        }
    }
}
