using System;
using System.Windows.Forms;

namespace RepoRepo
{
    public class Match
    {
        private DateTime MatchDate { get; set; } //<- check if datetime or timespan or right type ?
        private Team Team1 { get; set; }
        private Team Team2 { get; set; }
        private Label _dateLabel;

        /*
        private int score1;
        private int score2;
        */


        public Match(Team team1, Team team2, DateTime matchDate)
        {
                
        }


    }
}