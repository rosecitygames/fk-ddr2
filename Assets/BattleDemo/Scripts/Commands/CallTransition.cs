using RCG.Agents;
using RCG.Commands;
using RCG.States;

namespace RCG.Demo.BattleSimulator
{
    public class CallTransition : AbstractCommand
    {
        IStateTransitionHandler handler = null;
        string transition;

        protected override void OnStart()
        {
            if (handler != null && string.IsNullOrEmpty(transition) == false)
            {
                handler.HandleTransition(transition);
            }

            Complete();
        }

        public static ICommand Create(IStateTransitionHandler handler, string transition)
        {
            return new CallTransition
            {
                handler = handler,
                transition = transition
            };
        }
    }
}
