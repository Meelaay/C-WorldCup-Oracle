using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RepoRepo
{
    public class BasicTeam
    {
        public PictureBox Flag { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
        public string Group { get; set; }

        public BasicTeam(PictureBox flag, string name, string continent, string group)
        {
            Flag      = flag;
            Name      = name;
            Continent = continent;
            Group     = group;
        }
        

        public static PictureBox DeepCopy(PictureBox pb)
        {
            return new PictureBox { Name = pb.Name, Image = pb.Image, Size = pb.Size, SizeMode = pb.SizeMode };
        }

        public static BasicTeam DeepCopyTeam(BasicTeam basicTeam)
        {
            BasicTeam teamToReturn = new BasicTeam( 
                DeepCopy(basicTeam.Flag),
                string.Copy(basicTeam.Name),
                string.Copy(basicTeam.Continent),
                string.Copy(basicTeam.Group)
            );
            return teamToReturn;
        }

        public static List<BasicTeam> DeepCopyTeamsList(List<Team> teamsList)
        {
            List<BasicTeam> listToReturn = new List<BasicTeam>();

            foreach (var team in teamsList)
                listToReturn.Add(DeepCopyTeam((BasicTeam)team));

            return listToReturn;
        }

        protected Point GetPosition(Control c)
        {
            return c.FindForm().PointToClient(c.Parent.PointToScreen(c.Location));
        }

        public void MoveTeam(Point destination)
        {
            Flag.Left = destination.X;
            Flag.Top = destination.Y;
        }
    }
}