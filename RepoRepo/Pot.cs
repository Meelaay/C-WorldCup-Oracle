using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RepoRepo
{
    public class Pot
    {
        private List<Team> _potTeams;
        private static readonly Random _randomizer = new Random();

        //todo think of what should each pot have and know about in terms of properties, should it return a specific team for the group to process?

        public Pot(List<Team> list)
        {
            if (list.Count == 0 || list == null)
                throw new NullReferenceException("Constructor -> list arg passed is empty or null");
            
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