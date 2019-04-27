﻿using RCG.Advertisements;
using RCG.Items;
using RCG.States;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class Item : AbstractItem
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
            StopAllCoroutines();
            Destroy(gameObject);
        }

        public static IItem Create(GameObject gameObject, IItemData itemData, IAdvertisementBroadcaster broadcaster)
        {
            IItem agent = gameObject.AddComponent<Item>();
            agent.ItemData = itemData;
            agent.SetBroadcaster(broadcaster);
            return agent;
        }

    }
}