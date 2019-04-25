using RCG.Advertisements;
using RCG.Agents;
using RCG.States;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class Agent : AbstractAgent
    {
        const int CommandLayer0 = 0;
        const int CommandLayer1 = 1;

        protected override void InitStateMachine()
        {
            string onTargetFoundTransition = "OnTargetAdFound";
            string onEngagementCompletedTransition = "OnMapeCellEngagementCompleted";

            CommandableState wanderState = CommandableState.Create("Wander");
            wanderState.AddTransition(onTargetFoundTransition, "EngageTarget");
            wanderState.AddCommand(ChooseAdjacentLocation.Create(this), CommandLayer0);
            wanderState.AddCommand(MoveToTargetLocation.Create(this), CommandLayer0);
            wanderState.SetLayerLoopCount(CommandLayer0, -1);
            wanderState.AddCommand(DefaultAdvertisementHandler.Create(this, onTargetFoundTransition), CommandLayer1);
            stateMachine.AddState(wanderState);

            CommandableState engageTargetState = CommandableState.Create("EngageTarget");
            engageTargetState.AddTransition(onEngagementCompletedTransition, wanderState);
            engageTargetState.AddCommand(MoveToTargetLocation.Create(this), CommandLayer0);
            engageTargetState.AddCommand(EngageMapCell.Create(this, onEngagementCompletedTransition), CommandLayer0);
            engageTargetState.AddCommand(DefaultAdvertisementHandler.Create(this, onTargetFoundTransition), CommandLayer1);
            stateMachine.AddState(engageTargetState);

            stateMachine.SetState(wanderState);
        }

        public static IAgent Create(GameObject gameObject, IAgentData agentData, IAdvertisementBroadcaster broadcaster)
        {
            IAgent agent = gameObject.AddComponent<Agent>();
            agent.AgentData = agentData;
            agent.SetBroadcaster(broadcaster);
            return agent;
        }

        private void OnDrawGizmos()
        {
            DrawGizmoLineToTargetAdvertisement();
        }

        void DrawGizmoLineToTargetAdvertisement()
        {
            IAgent agent = this as IAgent;
            if (agent.TargetAdvertisement != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.localPosition, Map.CellToLocal(agent.TargetAdvertisement.Location));
            }     
        }
    }
}
