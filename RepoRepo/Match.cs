using System;

namespace RepoRepo
{
    public class Match
    {
        public DateTime DateOfMatch { get; set; } //<- check if datetime or timespan or right type ?
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }

    }
}