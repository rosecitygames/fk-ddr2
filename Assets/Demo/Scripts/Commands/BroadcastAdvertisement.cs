using RCG.Agents;
using RCG.Advertisements;
using RCG.Commands;
using System.Collections;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class BroadcastAdvertisement : AbstractCommand
    {
        IAgent agent;
        MonoBehaviour monoBehaviour;

        Coroutine broadcastCoroutine;

        protected override void OnStart()
        {
            bool isBroadcastable = (agent.BroadcastDistance > 0) && (agent.BroadcastInterval > 0);
            if (isBroadcastable)
            {
                StartBroadcast();
            }
            else
            {
                Complete();
            }
        }

        protected override void OnStop()
        {
            StopBroadcast();
        }

        protected override void OnDestroy()
        {
            StopBroadcast();
        }

        void StartBroadcast()
        {
            StopBroadcast();
            broadcastCoroutine = monoBehaviour.StartCoroutine(Broadcast());
        }

        void StopBroadcast()
        {
            if (broadcastCoroutine != null)
            {
                monoBehaviour.StopCoroutine(broadcastCoroutine);
            }
        }

        IEnumerator Broadcast()
        {
            while (isCompleted == false)
            {
                CreateAndBroadcastAdvertisement();
                yield return new WaitForSeconds(agent.BroadcastInterval);
            }
        }

        void CreateAndBroadcastAdvertisement()
        {
            IAdvertisement advertisement = Advertisement.Create(agent.Stats, agent.Location, agent.BroadcastDistance);
            agent.BroadcastAdvertisement(advertisement);
        }

        public static ICommand Create(AbstractAgent agent)
        {
            BroadcastAdvertisement command = new BroadcastAdvertisement
            {
                agent = agent,
                monoBehaviour = agent
            };

            return command;
        }
    }
}

