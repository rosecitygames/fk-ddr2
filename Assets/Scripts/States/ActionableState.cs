using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Actions;

namespace RCG.States
{
    public class ActionableState : AbstractState, IActionCollection
    {
        ActionPlayer actionPlayer;
        IActionEnumerator ActionPlayer
        {
            get
            {
                if (actionPlayer == null)
                {
                    actionPlayer = new ActionPlayer();
                }
                return actionPlayer;
            }
        }

        public void AddAction(IAction action)
        {
            ActionPlayer.AddAction(action);
        }

        public void AddAction(IAction action, int layer)
        {
            ActionPlayer.AddAction(action, layer);
        }

        public void RemoveAction(IAction action)
        {
            ActionPlayer.RemoveAction(action);
        }

        public void RemoveAction(IAction action, int layer)
        {
            ActionPlayer.RemoveAction(action, layer);
        }

        public bool HasAction(IAction action)
        {
            return ActionPlayer.HasAction(action);
        }

        public static ActionableState Create(string name)
        {
            ActionableState state = new ActionableState
            {
                stateName = name
            };
            return state;
        } 
    }
}