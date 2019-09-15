using IndieDevTools.Advertisements;
using IndieDevTools.Agents;
using IndieDevTools.Attributes;
using IndieDevTools.Commands;
using IndieDevTools.States;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Demo.BattleSimulator
{
    public class AttackHandler : AbstractCommand
    {
        ISoldier soldier = null;

        string onAttackedTransition;
        string onDeathTransition;

        protected override void OnStart()
        {
            AddEventHandler();
        }

        protected override void OnStop()
        {
            RemoveEventHandler();
        }

        protected override void OnDestroy()
        {
            RemoveEventHandler();
        }

        void AddEventHandler()
        {
            RemoveEventHandler();
            soldier.OnAttackReceived += HandleAttack;
        }

        void RemoveEventHandler()
        {
            soldier.OnAttackReceived -= HandleAttack;
        }

        void HandleAttack(IAgent attackingAgent)
        {
            attackingAgent.Description = "Attacking " + soldier.DisplayName;

            soldier.TargetMapElement = attackingAgent;

            int health = AttributesUtil.GetHealth(soldier);

            int attackStrength = AttributesUtil.GetRandomAttackStrength(attackingAgent);
            int defenseStrength = AttributesUtil.GetRandomDefenseStrength(soldier);

            int healthDecrement = attackStrength - defenseStrength;
            if (healthDecrement > 0)
            {          
                health -= healthDecrement;
                AttributesUtil.SetHealth(soldier, health);
            }


            if (health <= 0)
            {
                soldier.Description = "Killed by " + attackingAgent.DisplayName;
                soldier.HandleTransition(onDeathTransition);
            }
            else
            {
                soldier.Description = "Attacked by " + attackingAgent.DisplayName;// + "\nattackStrength = "+attackStrength+", defenseStrength = "+defenseStrength+", remaining health = " + health;
                soldier.HandleTransition(onAttackedTransition);
            }
        }
       
        public static ICommand Create(ISoldier soldier, string onAttackedTransition = "", string onDeathTransition = "")
        {
            return new AttackHandler
            {
                soldier = soldier,
                onAttackedTransition = onAttackedTransition,
                onDeathTransition = onDeathTransition
            };
        }
    }
}
