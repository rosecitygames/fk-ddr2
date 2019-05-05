using System;

namespace FloppyKnights.CardPlayers
{
    public interface ITurnTaker
    {
        void StartTurn();
        event Action<ITurnTaker> OnTurnCompleted;
    }
}
