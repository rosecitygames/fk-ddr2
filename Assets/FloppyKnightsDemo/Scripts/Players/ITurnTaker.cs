using System;

namespace FloppyKnights
{
    public interface ITurnTaker
    {
        void StartTurn();
        event Action<ITurnTaker> OnTurnCompleted;
    }
}
