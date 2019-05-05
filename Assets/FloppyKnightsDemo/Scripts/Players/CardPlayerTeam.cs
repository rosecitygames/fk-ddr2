using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;
using FloppyKnights.Agents;

namespace FloppyKnights.CardPlayers
{
    public class CardPlayerTeam : MonoBehaviour, ITeam
    {
        [SerializeField]
        string displayName = "";
        string IDescribable.DisplayName { get => displayName; }

        [SerializeField]
        [TextArea]
        string description = "";
        string IDescribable.Description{ get => description; }

        [SerializeField]
        int groupId = 0;
        int IGroupMember.GroupId{get => groupId; set => groupId = value; }

        [SerializeField]
        List<AbstractCardPlayer> teamMembers = new List<AbstractCardPlayer>();
        List<ICardPlayer> iTeamMembers = null;
        List<ICardPlayer> ITeam.TeamMembers { get => TeamMembers; }
        List<ICardPlayer> TeamMembers
        {
            get
            {
                if (iTeamMembers == null)
                {
                    iTeamMembers = new List<ICardPlayer>();
                    foreach (ICardPlayer teamMember in teamMembers)
                    {
                        teamMember.GroupId = groupId;
                        iTeamMembers.Add(teamMember);
                    }
                }
                return iTeamMembers;
            }
        }      

        void ITurnTaker.StartTurn()
        {
            StarTurn();
        }

        [ContextMenu("Start Turn")]
        void StarTurn()
        {
            Debug.Log("Team "+displayName + " is starting turn");
            bool isNoTeamMembers = TeamMembers.Count <= 0;
            if (isNoTeamMembers)
            {
                CallOnTurnCompleted();
                return;
            }

            ICardPlayer firstTeamMember = TeamMembers[0];
            StartTeamMemberTurn(firstTeamMember);
        }

        void StartTeamMemberTurn(ICardPlayer teamMember)
        {
            Debug.Log(teamMember.DisplayName + " is starting turn");
            AddTeamMemberEventHandlers(teamMember);
            teamMember.StartTurn();
        }

        void AddTeamMemberEventHandlers(ICardPlayer teamMember)
        {
            RemoveTeamMemberEventHandlers(teamMember);
            teamMember.OnTurnCompleted += TeamMember_OnTurnCompleted;
        }

        void RemoveTeamMemberEventHandlers(ICardPlayer teamMember)
        {
            teamMember.OnTurnCompleted -= TeamMember_OnTurnCompleted;
        }

        private void TeamMember_OnTurnCompleted(ITurnTaker turnTaker)
        {
            ICardPlayer teamMember = turnTaker as ICardPlayer;
            RemoveTeamMemberEventHandlers(teamMember);

            int teamMemberIndex = TeamMembers.IndexOf(teamMember);

            bool isLastTeamMember = teamMemberIndex >= TeamMembers.Count - 1;
            if (isLastTeamMember)
            {
                CallOnTurnCompleted();
            }
            else
            {
                int nextTeamMemberIndex = teamMemberIndex + 1;
                ICardPlayer nextTeamMember = TeamMembers[nextTeamMemberIndex];
                StartTeamMemberTurn(nextTeamMember);
            }
        }

        event Action<ITurnTaker> ITurnTaker.OnTurnCompleted
        {
            add
            {
                OnTurnCompleted += value;
            }
            remove
            {
                OnTurnCompleted -= value;
            }
        }

        Action<ITurnTaker> OnTurnCompleted;

        void CallOnTurnCompleted()
        {
            Debug.Log("Team " + displayName + " has completed turn");
            OnTurnCompleted?.Invoke(this);
        }
    }
}

