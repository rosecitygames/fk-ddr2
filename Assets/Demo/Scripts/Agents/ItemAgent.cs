using RCG.Advertisements;
using RCG.Agents;
using RCG.States;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class ItemAgent : AbstractAgent
    {

        protected override void InitStateMachine()
        {
            CommandableState broadcastState = CommandableState.Create("Broadcast");
            broadcastState.AddCommand(BroadcastAdvertisement.Create(this));
            stateMachine.AddState(broadcastState);
            stateMachine.SetState(broadcastState);
        }

        protected override void RemoveFromMap()
        {
            base.RemoveFromMap();
            Destroy(gameObject);
        }

        public static IAgent Create(GameObject gameObject, IAgentData agentData, IAdvertisementBroadcaster broadcaster)
        {
            IAgent agent = gameObject.AddComponent<ItemAgent>();
            agent.AgentData = agentData;
            agent.SetBroadcaster(broadcaster);
            return agent;
        }

        void OnDrawGizmos()
        {
            DrawBroadcastDistanceGizmo(Color.yellow);
        }
    
    }
}
