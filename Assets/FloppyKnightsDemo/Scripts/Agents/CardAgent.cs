using RCG.Commands;
using RCG.States;
using System;
using UnityEngine;

namespace FloppyKnights.Agents
{
    public class CardAgent : AbstractCardAgent
    {
        const int CommandLayer0 = 0;
        const int CommandLayer1 = 1;
        const int CommandLayer2 = 2;
        const int CommandLayer3 = 3;

        protected override void InitStateMachine()
        {
            // State objects
            CommandableState idleState = CommandableState.Create("Idle");
            stateMachine.AddState(idleState);

            CommandableState moveState = CommandableState.Create("Move");
            stateMachine.AddState(moveState);

            CommandableState atackState = CommandableState.Create("Attack");
            stateMachine.AddState(atackState);

            // Commands
            idleState.AddTransition("Move", moveState);

            moveState.AddTransition("Idle", idleState);
            moveState.AddCommand(WaitForTime.Create(this, 1.0f));
            moveState.AddCommand(CallTransition.Create(this, "Idle"));

            // Start
            stateMachine.SetState(idleState);
        }
    }
}

