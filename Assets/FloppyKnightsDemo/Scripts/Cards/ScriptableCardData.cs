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

        string IDescribable.DisplayName
        {
            get
            {
                return displayName;
            }
        }

        [SerializeField]
        [TextArea]
        string description = "";
        string IDescribable.Description
        {
            get
            {
                return description;
            }
        }

        [SerializeField]
        ScriptableBrain agentBrain = null;
        IBrain ICardData.AgentBrain
        {
            get
            {
                if (agentBrain == null)
                {
                    return new NullBrain();
                }
                else
                {
                    return (agentBrain as IBrain).Copy();
                }
            }
        }

        [SerializeField]
        Sprite agentSprite = null;
        Sprite ICardData.AgentSprite
        {
            get
            {
                return agentSprite;
            }
        }

        [SerializeField]
        AttributeCollection stats = new AttributeCollection();
        IAttributeCollection Stats
        {
            get
            {
                return stats as IAttributeCollection;
            }
        }

        List<IAttribute> IStatsCollection.Stats
        {
            get
            {
                return Stats.Attributes;
            }
        }

        IAttribute IStatsCollection.GetStat(string id)
        {
            return Stats.GetAttribute(id);
        }

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
