using RCG.Agents;
using RCG.Advertisements;
using RCG.Commands;
using RCG.Items;
using System.Collections;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class BroadcastAdvertisement : AbstractCommand
    {
        IAdvertisingMapElement advertisingMapElement = null;
        IAdvertisementReceiver excludeReceiver = null;

        MonoBehaviour monoBehaviour;

        Coroutine broadcastCoroutine;

        protected override void OnStart()
        {
            bool isBroadcastable = (advertisingMapElement.BroadcastDistance > 0) && (advertisingMapElement.BroadcastInterval > 0);
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
                yield return new WaitForSeconds(advertisingMapElement.BroadcastInterval);
            }
        }

        void CreateAndBroadcastAdvertisement()
        {
            IAdvertisement advertisement = Advertisement.Create(advertisingMapElement.Stats, advertisingMapElement.Location, advertisingMapElement.BroadcastDistance, advertisingMapElement.GroupId);
            advertisingMapElement.BroadcastAdvertisement(advertisement, excludeReceiver);
        }

        public static ICommand Create(AbstractItem item)
        {
            BroadcastAdvertisement command = new BroadcastAdvertisement
            {
                advertisingMapElement = item,
                monoBehaviour = item
            };

            return command;
        }

        public static ICommand Create(AbstractAgent agent)
        {
            BroadcastAdvertisement command = new BroadcastAdvertisement
            {
                advertisingMapElement = agent,
                excludeReceiver = agent,
                monoBehaviour = agent
            };

            return command;
        }
    }
}

