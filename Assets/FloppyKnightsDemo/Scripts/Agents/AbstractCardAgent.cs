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
        ICardData ICardAgent.CardData { get => CardData; set => CardData = value; }
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
                    InitSprite();
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

        string IDescribable.DisplayName { get => DisplayName; }
        protected string DisplayName { get => CardData.DisplayName; }

        string IDescribable.Description { get => Description; }
        protected string Description { get => CardData.Description; }

        List<IAttribute> IStatsCollection.Stats { get => Stats; }
        protected List<IAttribute> Stats { get => StatsCollection.Attributes; }
        IAttributeCollection StatsCollection
        {
            get
            {
                IAttributeCollection statsCollection = new AttributeCollection(CardData.Stats);
                // TODO return new list that combines base CardData stats with temp buff stats and turn effect stats
                return statsCollection;
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
        IMap IMapElement.Map { get => Map; set => Map = value; }
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

        void IMapElement.AddToMap(IMap map) => AddToMap(map);
        protected virtual void AddToMap(IMap map)
        {
            if (map != null)
            {
                Map.RemoveElement(this);
            }
            this.map = map;
            Map.AddElement(this);
        }

        void IMapElement.RemoveFromMap() => RemoveFromMap();
        protected virtual void RemoveFromMap()
        {
            Map.RemoveElement(this);
        }

        float IMapElement.Distance(IMapElement otherMapElement)
        {
            return Vector3Int.Distance(otherMapElement.Location, Location);
        }

        int IMapElement.InstanceId => gameObject.GetInstanceID();

        int IMapElement.SortingOrder => SortingOrder;
        protected virtual int SortingOrder => Mathf.RoundToInt(Position.y * Map.CellSize.y * -100.0f);

        Vector3Int ILocatable.Location => Location;
        protected virtual Vector3Int Location => Map.LocalToCell(Position);

        Vector3 IPositionable.Position { get => Position; set => Position = value; }
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

        IMapElement ICardAgent.TargetMapElement { get => TargetMapElement; set => TargetMapElement = value; }
        protected virtual IMapElement TargetMapElement { get; set; }

        Vector3Int ICardAgent.TargetLocation { get => TargetLocation; set => TargetLocation = value; }
        protected virtual Vector3Int TargetLocation { get; set; }

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

        protected Action OnIdleStarted;

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
        int IGroupMember.GroupId { get => GroupId; set => GroupId = value; }
        protected int GroupId { get { return groupId; } set { groupId = value; } }
        
        // Turn effect implementations
        // How this agent effects other agent turns
        List<ITurnEffect> ITurnEffecter.TurnEffects { get => TurnEffects; }
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
        protected IStateMachine stateMachine = StateMachine.Create();

        protected virtual void InitStateMachine() { }

        void IStateTransitionHandler.HandleTransition(string transitionName)
        {
            HandleTransition(transitionName);
        }

        protected virtual void HandleTransition(string transitionName)
        {
            stateMachine.HandleTransition(transitionName);
            // TODO: Probably a better way to call this
            if (transitionName == "Idle")
            {
                OnIdleStarted?.Invoke();
            }
        }

        // Sprite implementations
        protected virtual void InitSprite()
        {
            // TODO : If you want something fancier than a sprite, then I recommend making a separate component for that and pass data and call methods on it via commands
            if (CardData.AgentSprite == null) return;

            SpriteRenderer spriteRender = GetComponentInChildren<SpriteRenderer>();
            if (spriteRender != null)
            {
                spriteRender.sprite = CardData.AgentSprite;
            }
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
            InitSprite();
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

