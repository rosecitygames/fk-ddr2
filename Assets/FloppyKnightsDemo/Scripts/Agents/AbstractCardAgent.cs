using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FloppyKnights.Cards;
using FloppyKnights.TurnEffects;
using RCG.Attributes;
using RCG.Maps;
using RCG.States;

namespace FloppyKnights.Agents
{
    public class AbstractCardAgent : MonoBehaviour, ICardAgent
    {
        // CardData implementations
        [SerializeField]
        ScriptableCardData data = null;

        ICardData cardData;

        ICardData ICardAgent.CardData
        {
            get
            {
                return CardData;
            }
            set
            {
                CardData = value;
            }
        }

        protected ICardData CardData
        {
            get
            {
                if (cardData == null)
                {
                    InitCardData();
                }
                return cardData;
            }
            set
            {
                if (cardData != value)
                {
                    cardData = value;
                    SetSprite();
                }          
            }
        }

        protected virtual void InitCardData()
        {
            if (data == null)
            {
                cardData = new NullCardData();
            }
            else
            {
                cardData = (data as ICardData).Copy();
            }
        }

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
                return CardData.DisplayName;
            }
        }

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
                return CardData.Description;
            }
        }

        List<IAttribute> IStatsCollection.Stats
        {
            get
            {
                return Stats;
            }
        }

        IAttributeCollection StatsCollection
        {
            get
            {
                IAttributeCollection statsCollection = new AttributeCollection(CardData.Stats);
                // TODO return new list that combines base CardData stats with temp buff stats and turn effect stats
                return statsCollection;
            }
        }

        protected List<IAttribute> Stats
        {
            get
            {
                return StatsCollection.Attributes;          
            }
        }

        IAttribute IStatsCollection.GetStat(string id)
        {
            return GetStat(id);
        }

        protected virtual IAttribute GetStat(string id)
        {
            return StatsCollection.GetAttribute(id);
        }

        // Map implementations
        IMap map;

        IMap IMapElement.Map
        {
            get
            {
                return Map;
            }
            set
            {
                Map = value;
            }
        }

        protected IMap Map
        {
            get
            {
                if (map == null)
                {
                    InitMap();
                }
                return map;
            }
            set
            {
                map = value;
            }
        }

        protected virtual void InitMap()
        {
            map = GetComponentInParent<IMap>() ?? NullMap.Create();
            map.AddElement(this);
        }

        void IMapElement.AddToMap(IMap map)
        {
            AddToMap(map);
        }

        protected virtual void AddToMap(IMap map)
        {
            if (map != null)
            {
                Map.RemoveElement(this);
            }
            this.map = map;
            Map.AddElement(this);
        }

        void IMapElement.RemoveFromMap()
        {
            RemoveFromMap();
        }
        protected virtual void RemoveFromMap()
        {
            Map.RemoveElement(this);
        }

        float IMapElement.Distance(IMapElement otherMapElement)
        {
            return Vector3Int.Distance(otherMapElement.Location, Location);
        }

        int IMapElement.SortingOrder { get { return SortingOrder; } }
        protected virtual int SortingOrder { get { return Mathf.RoundToInt(Position.y * Map.CellSize.y * -100.0f); } }

        Vector3Int ILocatable.Location { get { return Location; } }
        protected virtual Vector3Int Location { get { return Map.LocalToCell(Position); } }

        Vector3 IPositionable.Position { get { return Position; } set { Position = value; } }
        protected virtual Vector3 Position
        {
            get
            {
                return transform.localPosition;
            }
            set
            {
                Vector3Int currentLocation = Location;
                Vector3Int newLocation = Map.LocalToCell(value);

                transform.localPosition = value;

                if (currentLocation != newLocation)
                {
                    Map.AddElement(this);
                }
            }
        }

        // Card Agent implementations

        IMapElement ICardAgent.TargetMapElement
        {
            get
            {
                return TargetMapElement;
            }
            set
            {
                TargetMapElement = value;
            }
        }
        protected IMapElement TargetMapElement { get; set; }

        Vector3Int ICardAgent.TargetLocation
        {
            get
            {
                return TargetLocation;
            }
            set
            {
                TargetLocation = value;
            }
        }
        protected Vector3Int TargetLocation { get; set; }

        event Action ICardAgent.OnIdleStarted
        {
            add
            {
                OnIdleStarted += value;
            }
            remove
            {
                OnIdleStarted -= value;
            }
        }

        Action OnIdleStarted;

        void ICardAgent.Move(Vector3Int location)
        {
            Move(location);
        }
        protected virtual void Move(Vector3Int location)
        {
            TargetLocation = location;
            HandleTransition("Move");
        }

        void ICardAgent.Attack(ICardAgent targetAgent)
        {
            Attack(targetAgent);
        }
        protected virtual void Attack(ICardAgent targetAgent)
        {
            TargetMapElement = targetAgent;
            HandleTransition("Attack");
        }

        // TODO: Make temp buff list that gets combined with base stats
        void ICardAgent.Buff(IAttributeCollection attributeCollection)
        {

        }


        // Group Member implementations
        [SerializeField]
        int groupId;
        protected int GroupId { get { return groupId; } set { groupId = value; } }
        int IGroupMember.GroupId
        {
            get
            {
                return GroupId;
            }
            set
            {
                GroupId = value;
            }
        }

        // Turn effect implementations
        // How this agent effects other agent turns
        List<ITurnEffect> ITurnEffecter.TurnEffects
        {
            get
            {
                return TurnEffects;
            }
        }

        protected List<ITurnEffect> TurnEffects { get; set; }

        // Turn effect collector implementations
        // How this agent is effected by other turn effectors (anything on the map that is implement ITurnEffecter)
        // List should be cleared before setting each turn
        protected List<ITurnEffect> turnEffectBuffs = new List<ITurnEffect>();

        void ITurnEffectCollector.AddTurnEffect(ITurnEffect turnEffect)
        {
            AddTurnEffect(turnEffect);
        }

        protected virtual void AddTurnEffect(ITurnEffect turnEffect)
        {
            turnEffectBuffs.Add(turnEffect);
        }

        void ITurnEffectCollector.RemoveTurnEffect(ITurnEffect turnEffect)
        {
            RemoveTurnEffect(turnEffect);
        }

        protected virtual void RemoveTurnEffect(ITurnEffect turnEffect)
        {
            turnEffectBuffs.Remove(turnEffect);
        }

        void ITurnEffectCollector.ClearTurnEffects()
        {
            ClearTurnEffects();
        }

        protected virtual void ClearTurnEffects()
        {
            turnEffectBuffs.Clear();
        }

        // State Machine implementations
        // TODO : Will probably want to pass "brains" state machine via card data.
        // That way card data can be swapped seamlessly without needing to create multiple
        // concrete implementations of card agents.
        //
        // New brain state machine should still handle the transition.

        protected IStateMachine stateMachine = StateMachine.Create();

        protected virtual void InitStateMachine() { }

        void IStateTransitionHandler.HandleTransition(string transitionName)
        {
            HandleTransition(transitionName);
        }
        protected virtual void HandleTransition(string transitionName)
        {
            stateMachine.HandleTransition(transitionName);
        }

        // Sprite implementations
        protected virtual void SetSprite()
        {
            // TODO : Set sprite renderer component sprite value from CardData sprite value
            // TODO : If you want something fancier than a sprite, then I recommend making a separate component for that and pass data and call methods on it via commands
        }

        // Initialization
        void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            InitCardData();
            InitMap();
            InitStateMachine();
        }

        // Cleanup
        void OnDestroy()
        {
            Cleanup();
        }

        protected virtual void Cleanup()
        {
            RemoveFromMap();

            if (stateMachine != null)
            {
                stateMachine.Destroy();
            }
        }
    }
}

