using RCG.Agents;
using RCG.Advertisements;
using RCG.Commands;
using System.Collections;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class BroadcastAdvertisement : AbstractCommand
    {
        IAdvertisingMapElement advertisingMapElement;
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
            IAdvertisement advertisement = Advertisement.Create(advertisingMapElement.Stats, advertisingMapElement.Location, advertisingMapElement.BroadcastDistance);
            advertisingMapElement.BroadcastAdvertisement(advertisement);
        }

        public static ICommand Create(IAdvertisingMapElement advertisingMapElement)
        {
            return Create(advertisingMapElement, advertisingMapElement as MonoBehaviour);
        }

        public static ICommand Create(IAdvertisingMapElement advertisingMapElement, MonoBehaviour monoBehaviour)
        {
            BroadcastAdvertisement command = new BroadcastAdvertisement
            {
                advertisingMapElement = advertisingMapElement,
                monoBehaviour = monoBehaviour
            };

            return command;
        }
    }
}

