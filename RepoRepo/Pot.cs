using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RepoRepo
{
    public class Pot
    {
        public List<Team> _potTeams;//todo make private and fix where used public
        //TODO make _potTeams a List<List<Team>> each 

        public List<Team> SubSetAfrica = new List<Team>();
        public List<Team> SubSetAsia = new List<Team>();
        public List<Team> SubSetEurope = new List<Team>();
        public List<Team> SubSetNorthAmerica = new List<Team>();
        public List<Team> SubSetSouthAmerica = new List<Team>();

        private static readonly Random _randomizer = new Random();
        
        //todo think of what should each pot have and know about in terms of properties, should it return a specific team for the group to process?
        public void SortInSubsets()
        {
            foreach (var team in _potTeams)
            {
                if (team.IsAfrica){ SubSetAfrica.Add(team); continue; }
                if (team.IsAsia) { SubSetAsia.Add(team); continue; }
                if (team.IsEurope) { SubSetEurope.Add(team); continue; }
                if (team.IsNorthAmerica) { SubSetNorthAmerica.Add(team); continue; }
                if (team.IsSouthAmerica) { SubSetSouthAmerica.Add(team); continue; }
            }
        }

        public Pot(List<Team> list)
        {
            if (list.Count == 0 || list == null)
                throw new NullReferenceException("Pot::Pot() -> list arg passed is empty or null");
            
            _potTeams = list;
        }

        public void RemoveTeamFromPot(Team team)
        {
            _potTeams.Remove(team);
        }

        public Team GetRandomTeam()
        {
            return _potTeams[_randomizer.Next(_potTeams.Count)];
        }

        public Team GetRandomTeamFromSubSet(string continent)
        {
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
            }
            throw new Exception("Pot::GetRandomTeamFromSubset() -> invalid continent");
        }

        public List<PictureBox> GetPictureBoxes()
        {
            if (_potTeams.Count == 0 || _potTeams == null)
                throw new NullReferenceException("GetPictureBoxes -> _potTeams is empty or null");
            
            List<PictureBox> pictureBoxes = new List<PictureBox>();
            foreach (var team in _potTeams)
            {
                pictureBoxes.Add(team.Flag);
            }

            return pictureBoxes;

        }
    }
}