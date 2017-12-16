using System;
using System.Collections.Generic;
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
                String.Copy(basicTeam.Name),
                String.Copy(basicTeam.Continent),
                String.Copy(basicTeam.Group)
            );
            return teamToReturn;
        }

        public static List<BasicTeam> DeepCopyTeamsList(List<Team> teamsList)
        {
            List<BasicTeam> listToReturn = new List<BasicTeam>();
            foreach (var team in teamsList)
            {
                listToReturn.Add(DeepCopyTeam((BasicTeam)team));
            }
            return listToReturn;
        }

    }
}