using System;
using System.Windows.Forms;

namespace RepoRepo
{
    public class Match
    {
        public DateTime MatchDate { get; } //<- check if datetime or timespan or right type ?
        public Team Team1 { get; }
        public Team Team2 { get; }

        public Label DateLabel = new Label();

        /*
        private int score1;
        private int score2;
        */


        public Match(Team team1, Team team2, DateTime matchDate)
        {
            Team1 = team1; Team2 = team2;
            MatchDate = matchDate;
            DateLabel.Text = matchDate.ToLongDateString();
        }


    }
}