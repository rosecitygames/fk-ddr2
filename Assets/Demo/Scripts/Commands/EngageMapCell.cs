﻿using RCG.Advertisements;
using RCG.Agents;
using RCG.Attributes;
using RCG.Commands;
using RCG.States;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Demo.Simulator
{
    public class EngageMapCell : AbstractCommand
    {
        IState state = null;
        IAgent agent = null;

        protected override void OnStart()
        {
            // Get all cell elements and interact with highest ranked element
        }

        protected override void OnStop()
        {
            
        }

        public static ICommand Create(IState state, IAgent agent)
        {
            return new EngageMapCell
            {
                state = state,
                agent = agent
            };
        }
    }
}