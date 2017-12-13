using System;
using System.Windows.Forms;

namespace RepoRepo
{
    public class Match
    {
        private DateTime DateOfMatch { get; set; } //<- check if datetime or timespan or right type ?
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        private Label _dateLabel;
        private int score1;
        private int score2;


        public Match()
        {
            
        }
    }
}