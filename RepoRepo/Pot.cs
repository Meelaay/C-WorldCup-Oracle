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
        
        private static readonly List<int> stateArray = new List<int> {1,2,3,4,5,6,7,8,9};
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
            //make inline add add add add ..
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

        public int GetTotalNumberOfTeams()
        {
            int n = 0;
            foreach (var subset in _potTeams)
                foreach (var unused in subset)
                    n++;
            return n;
        }

        public bool ContainsContinent(string continent)
        {
            return _potTeams[ContinentIndexInPot(continent)].Count != 0;
        }

        public Team GetRandomTeamFromSubSet(string continent)
        {
            int i = ContinentIndexInPot(continent);
            int size = _potTeams[i].Count;
            return _potTeams[i][_randomizer.Next(size)];
            
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