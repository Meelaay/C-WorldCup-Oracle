﻿using System;
using System.Windows.Forms;

namespace RepoRepo
{
    public class Match
    {
        //private int _matchID; -> could be used later to identify matches or maybe use just names

        public DateTime MatchDate { get; } //<- check if datetime or timespan or right type ?
        public BasicTeam Team1 { get; }
        public BasicTeam Team2 { get; }

        public Label DateLabel = new Label();

        public TextBox score1 { get; }
        public TextBox score2 { get; }


        

        public Match(BasicTeam team1, BasicTeam team2, DateTime matchDate)
        {
            Team1 = BasicTeam.DeepCopyTeam(team1);
            Team2 = BasicTeam.DeepCopyTeam(team2);

            MatchDate = matchDate;
            DateLabel.AutoSize = true;
            DateLabel.Text = matchDate.ToLongDateString();
        }
    }
}