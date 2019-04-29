using System;

namespace RCG.Demo.FloppyKnights
{
    public interface ITurnTaker
    {
        void StartTurn();
        event Action<ITurnTaker> OnTurnCompleted;
    }
}
