using RCG.Advertisements;
using RCG.Agents;
using RCG.States;
using System;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class Agent : AbstractAgent, IAttackReceiver
    {
        const int CommandLayer0 = 0;
        const int CommandLayer1 = 1;

        protected override void InitStateMachine()
        {
            CommandableState wanderState = CommandableState.Create("Wander");
            CommandableState inspectTargetLocationState = CommandableState.Create("InspectTargetLocation");
            CommandableState attackEnemyState = CommandableState.Create("AttackEnemyState");
            CommandableState pickupItemState = CommandableState.Create("pickupItem");

            // Wander State

            string onTargetFoundTransition = "OnTargetAdFound";
                        
            wanderState.AddTransition(onTargetFoundTransition, inspectTargetLocationState);
            wanderState.AddCommand(ChooseAdjacentLocation.Create(this), CommandLayer0);
            wanderState.AddCommand(MoveToTargetLocation.Create(this), CommandLayer0);
            wanderState.SetLayerLoopCount(CommandLayer0, -1);
            wanderState.AddCommand(DefaultAdvertisementHandler.Create(this, onTargetFoundTransition), CommandLayer1);
            stateMachine.AddState(wanderState);

            // Inspect Target Location State

            string onEnemeyFoundTransition = "OnEnemyFound";
            string onItemFoundTransition = "OnItemFound";
            string onNothingFoundTransition = "OnNothingFound";
            
            inspectTargetLocationState.AddTransition(onEnemeyFoundTransition, attackEnemyState);
            inspectTargetLocationState.AddTransition(onItemFoundTransition, pickupItemState);
            inspectTargetLocationState.AddTransition(onNothingFoundTransition, wanderState);
            inspectTargetLocationState.AddCommand(MoveToTargetLocation.Create(this), CommandLayer0);
            inspectTargetLocationState.AddCommand(ChooseTargetAgentAtLocation.Create(this), CommandLayer0);
            inspectTargetLocationState.AddCommand(InspectTargetAgent.Create(this, onEnemeyFoundTransition, onItemFoundTransition, onNothingFoundTransition), CommandLayer0);
            inspectTargetLocationState.AddCommand(DefaultAdvertisementHandler.Create(this, onTargetFoundTransition), CommandLayer1);
            stateMachine.AddState(inspectTargetLocationState);

            stateMachine.SetState(wanderState);
        }

        void IAttackReceiver.ReceiveAttack(IAgent attackingAgent)
        {
            OnAttackReceived?.Invoke(attackingAgent);
        }

        public Action<IAgent> OnAttackReceived;

        public static IAgent Create(GameObject gameObject, IAgentData agentData, IAdvertisementBroadcaster broadcaster)
        {
            IAgent agent = gameObject.AddComponent<Agent>();
            agent.AgentData = agentData;
            agent.SetBroadcaster(broadcaster);
            return agent;
        }

        private void OnDrawGizmos()
        {
            DrawLineToTargetAdvertisementGizmo(Color.blue);
            DrawBroadcastDistanceGizmo(Color.green);
        }
   
    }
}
