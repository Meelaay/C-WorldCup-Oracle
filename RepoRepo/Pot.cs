using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RepoRepo
{
    public class Pot
    {
        public List<List<Team>> _potTeams = new List<List<Team>>(capacity: 5);//todo make private and fix where used public


        private static readonly Random _randomizer = new Random();
        
        //todo think of what should each pot have and know about in terms of properties, should it return a specific team for the group to process?
        

        private int ContinentIndexInPot(string continent)
        {
            switch (continent)
            {
                case "africa":
                    return 0;
                case "asia":
                    return 1;
                case "europe":
                    return 2;
                case "northamerica":
                    return 3;
                case "southamerica":
                    return 4;
            }
            throw new Exception("Pot::ContinentIndexInPot() -> invalid continent");
        }
        public Pot(List<Team> teamsList)
        {
            if (teamsList.Count == 0 || teamsList == null)
                throw new NullReferenceException("Pot::Pot() -> list arg passed is empty or null");
            //extract a method (for i .. <<>>.capacity) INIT_POTTEAMS
            _potTeams.Add(new List<Team>()); _potTeams.Add(new List<Team>()); _potTeams.Add(new List<Team>()); _potTeams.Add(new List<Team>()); _potTeams.Add(new List<Team>());
            foreach (var team in teamsList)
            {
                _potTeams[ContinentIndexInPot(team.Continent)].Add(team);
            }

        }

        public void RemoveTeamFromPot(Team team)
        {
            _potTeams[ContinentIndexInPot(team.Continent)].Remove(team);
        }

        public Team GetRandomTeam()
        {
            int a = _randomizer.Next(5);
            return _potTeams[a][_randomizer.Next(_potTeams[a].Count)];
        }

        public Team GetRandomTeamFromSubSet(string continent)
        {/*
            switch (continent)
            {
                case "africa":
                    return SubSetAfrica[_randomizer.Next(SubSetAfrica.Count)];
                case "asia":
                    return SubSetAsia[_randomizer.Next(SubSetAsia.Count)];
                case "europe":
                    return SubSetEurope[_randomizer.Next(SubSetEurope.Count)];
                case "northamerica":
                    return SubSetNorthAmerica[_randomizer.Next(SubSetNorthAmerica.Count)];
                case "southamerica":
                    return SubSetSouthAmerica[_randomizer.Next(SubSetSouthAmerica.Count)];
            }*/
            throw new Exception("Pot::GetRandomTeamFromSubset() -> invalid continent");
        }

        public List<PictureBox> GetPictureBoxes()
        {
            if (_potTeams.Count == 0 || _potTeams == null)
                throw new NullReferenceException("GetPictureBoxes -> _potTeams is empty or null");
            
            List<PictureBox> pictureBoxes = new List<PictureBox>();
            foreach (var teamSet in _potTeams)
            {
                foreach (var team in teamSet)
                {
                    pictureBoxes.Add(team.Flag);
                }
            }

            return pictureBoxes;

        }
    }
}