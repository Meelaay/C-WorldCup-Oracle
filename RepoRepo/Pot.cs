using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RepoRepo
{
    public class Pot
    {
        private List<Point> _teamPositions = new List<Point>(capacity: 5);
        private List<Team> _potTeams;

        //todo think of what should each pot have and know about in terms of properties, should it return a specific team for the group to process?

        public Pot(List<Team> list)
        {
            if (list.Count == 0 || list == null)
                throw new NullReferenceException("Constructor -> list arg passed is empty or null");
            
            _potTeams = list;
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