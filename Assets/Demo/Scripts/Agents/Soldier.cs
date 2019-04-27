using RCG.Advertisements;
using RCG.Agents;
using RCG.States;
using System;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class Soldier : AbstractAgent, ISoldier
    {
        const int CommandLayer0 = 0;
        const int CommandLayer1 = 1;
        const int CommandLayer2 = 2;
        const int CommandLayer3 = 3;

        protected override void InitStateMachine()
        {
            // State objects
            CommandableState wanderState = CommandableState.Create("Wander");
            stateMachine.AddState(wanderState);

            CommandableState inspectTargetLocationState = CommandableState.Create("InspectTargetLocation");
            stateMachine.AddState(inspectTargetLocationState);

            CommandableState attackEnemyState = CommandableState.Create("AttackEnemyState");
            stateMachine.AddState(attackEnemyState);

            CommandableState deathState = CommandableState.Create("DeathState");
            stateMachine.AddState(deathState);

            CommandableState pickupItemState = CommandableState.Create("pickupItem");
            stateMachine.AddState(pickupItemState);

            // Transitions strings
            string onTargetFoundTransition = "OnTargetAdFound";
            string onAttackedTransition = "OnAttacked";
            string onEnemyKilledTransition = "OnEnemyKilled";
            string onDeathTransition = "OnDeath";
            string onEnemeyFoundTransition = "OnEnemyFound";
            string onItemFoundTransition = "OnItemFound";
            string onNothingFoundTransition = "OnNothingFound";
            string onPickupCompleted = "OnPickupCompleted";

            // Wander State                       
            wanderState.AddTransition(onTargetFoundTransition, inspectTargetLocationState);
            wanderState.AddTransition(onAttackedTransition, attackEnemyState);
            wanderState.AddTransition(onDeathTransition, deathState);
            wanderState.AddCommand(ChooseAdjacentLocation.Create(this), CommandLayer0);
            wanderState.AddCommand(MoveToTargetLocation.Create(this), CommandLayer0);
            wanderState.AddCommand(WaitForTime.Create(this, 0.25f), CommandLayer0);
            wanderState.SetLayerLoopCount(CommandLayer0, -1);
            wanderState.AddCommand(BroadcastAdvertisement.Create(this), CommandLayer1);
            wanderState.AddCommand(AdvertisementHandler.Create(this, onTargetFoundTransition), CommandLayer2);
            wanderState.AddCommand(AttackHandler.Create(this, onAttackedTransition, onDeathTransition), CommandLayer3);
            
            // Inspect Target Location State
            inspectTargetLocationState.AddTransition(onEnemeyFoundTransition, attackEnemyState);
            inspectTargetLocationState.AddTransition(onItemFoundTransition, pickupItemState);
            inspectTargetLocationState.AddTransition(onNothingFoundTransition, wanderState);
            inspectTargetLocationState.AddTransition(onAttackedTransition, attackEnemyState);
            inspectTargetLocationState.AddTransition(onDeathTransition, deathState);
            inspectTargetLocationState.AddCommand(MoveToTargetLocation.Create(this), CommandLayer0);
            inspectTargetLocationState.AddCommand(WaitForTime.Create(this, 0.25f), CommandLayer0);
            inspectTargetLocationState.AddCommand(ChooseTargetMapElmentAtLocation.Create(this), CommandLayer0);
            inspectTargetLocationState.AddCommand(InspectTargetMapElement.Create(this, onEnemeyFoundTransition, onItemFoundTransition, onNothingFoundTransition), CommandLayer0);
            inspectTargetLocationState.AddCommand(BroadcastAdvertisement.Create(this), CommandLayer1);
            inspectTargetLocationState.AddCommand(AdvertisementHandler.Create(this), CommandLayer2);
            inspectTargetLocationState.AddCommand(AttackHandler.Create(this, onAttackedTransition, onDeathTransition), CommandLayer3);

            // Attack Enemey state
            attackEnemyState.AddTransition(onEnemyKilledTransition, wanderState);
            attackEnemyState.AddTransition(onDeathTransition, deathState);
            attackEnemyState.AddCommand(AttackTargetMapElement.Create(this, onEnemyKilledTransition), CommandLayer0);
            attackEnemyState.AddCommand(WaitForTime.Create(this, 0.5f), CommandLayer0);
            attackEnemyState.SetLayerLoopCount(CommandLayer0, -1);
            attackEnemyState.AddCommand(BroadcastAdvertisement.Create(this), CommandLayer1);
            attackEnemyState.AddCommand(AttackHandler.Create(this, onAttackedTransition, onDeathTransition), CommandLayer2);

            // Death state
            deathState.AddCommand(Die.Create(this));

            // Pickup Item state
            pickupItemState.AddTransition(onPickupCompleted, wanderState);
            pickupItemState.AddTransition(onAttackedTransition, attackEnemyState);
            pickupItemState.AddTransition(onDeathTransition, deathState);
            pickupItemState.AddCommand(PickupItem.Create(this));
            pickupItemState.AddCommand(WaitForTime.Create(this, 0.5f), CommandLayer0);
            pickupItemState.AddCommand(CallTransition.Create(this, onPickupCompleted), CommandLayer0);
            pickupItemState.AddCommand(AttackHandler.Create(this, onAttackedTransition, onDeathTransition), CommandLayer1);

            stateMachine.SetState(wanderState);
        }

        void IAttackReceiver.ReceiveAttack(IAgent attackingAgent)
        {
            OnAttackReceived?.Invoke(attackingAgent);
        }

        event Action<IAgent> ISoldier.OnAttackReceived
        {
            add
            {
                OnAttackReceived += value;
            }
            remove
            {
                OnAttackReceived -= value;
            }
        }

        Action<IAgent> OnAttackReceived;

        protected override void RemoveFromMap()
        {
            base.RemoveFromMap();
            StopAllCoroutines();
            Destroy(gameObject);
        }

        public static IAgent Create(GameObject gameObject, IAgentData agentData, IAdvertisementBroadcaster broadcaster)
        {
            IAgent agent = gameObject.AddComponent<Soldier>();
            agent.AgentData = agentData;
            agent.SetBroadcaster(broadcaster);
            return agent;
        }
    }
}
