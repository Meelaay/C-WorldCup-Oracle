using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RepoRepo
{
    public class Pot
    {
        private List<Point> _teamPositions = new List<Point>(capacity: 5);
        private List<Team> _potTeams;

        public Pot(List<Team> list)
        {
            _potTeams = list;
        }

        public List<PictureBox> GetPictureBoxes()
        {
            List<PictureBox> pictureBoxes = new List<PictureBox>();
            foreach (var team in _potTeams)
            {
                pictureBoxes.Add(team.Flag);
            }

            return pictureBoxes;

        }
    }
}