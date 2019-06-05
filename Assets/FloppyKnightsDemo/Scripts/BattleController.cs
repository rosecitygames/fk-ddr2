using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FloppyKnights.CardPlayers;

namespace FloppyKnights
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField]
        List<CardPlayerTeam> teams = new List<CardPlayerTeam>();

        List<ITurnTaker> turnTakers = new List<ITurnTaker>();
        
        private void Start()
        {
            Invoke("StartBattle", 1.0f);
        }

        [ContextMenu("Start Battle")]
        void StartBattle()
        {
            if (teams.Count <= 0) return;

            turnTakers.Clear();
            foreach(ITurnTaker turnTaker in teams)
            {
                turnTakers.Add(turnTaker);
            }

            AddTurnTakerEventHandlers();

            ITurnTaker firstTurnTaker = turnTakers[0];
            firstTurnTaker.StartTurn();
        }

        void AddTurnTakerEventHandlers()
        {
            RemoveTurnTakerEventHandlers();
            foreach (ITurnTaker turnTaker in turnTakers)
            {
                turnTaker.OnTurnCompleted += TurnTaker_OnTurnCompleted;
            }
        }

        void RemoveTurnTakerEventHandlers()
        {
            foreach (ITurnTaker turnTaker in turnTakers)
            {
                turnTaker.OnTurnCompleted -= TurnTaker_OnTurnCompleted;
            }
        }

        private void TurnTaker_OnTurnCompleted(ITurnTaker obj)
        {
            int turnTakerIndex = turnTakers.IndexOf(obj);
            int nextTurnTakerIndex = 0;

            bool isValidTurnTaker = turnTakerIndex >= 0;
            if (isValidTurnTaker)
            {
                nextTurnTakerIndex = (turnTakerIndex + 1) % turnTakers.Count;
            }

            ITurnTaker nextTurnTaker = turnTakers[nextTurnTakerIndex];
            nextTurnTaker.StartTurn();
        }
    }
}
