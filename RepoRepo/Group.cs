using System.Collections.Generic;
using System.Drawing;

namespace RepoRepo
{
    public class Group
    {
        private List<Team> _group = new List<Team>(capacity: 5);

        //todo group should it know about pots logic ? or just stuff inside itself ? 
        //todo is it the engine property to manage right team from right pot to the right group ? so should it have the logic ?

        public readonly Point BORDERPOINT1;
        public readonly Point BORDERPOINT2;
        public readonly Point BORDERPOINT3;
        public readonly Point BORDERPOINT4;

        public bool IsComplete { get; set; }

        public bool IsAfrica { get; set; }
        public bool IsAsia { get; set; }
        public bool IsEurope { get; set; }
        public bool IsNorthAmerica { get; set; }
        public bool IsSouthAmerica { get; set; }

        public Group(Point border1, Point border2, Point border3, Point border4)
        {
            //to do set initial values for bools
            _group.Add(null);
            BORDERPOINT1 = border1;
            BORDERPOINT2 = border2;
            BORDERPOINT3 = border3;
            BORDERPOINT4 = border4;
        }

        public bool AddTeamToGroup(Team teamToAdd)
        {
            //logic to add or not the team
            if (teamToAdd.IsAfrica)
            {
                if (IsAfrica)
                {
                    return false;
                }
                _group.Add(teamToAdd);
            }
            else if (teamToAdd.IsAsia)
            {
                if (IsAsia)
                {
                    return false;
                }
                _group.Add(teamToAdd);
            }
            else if (teamToAdd.IsEurope)
            {
                _group.Add(teamToAdd);
            }
            else if (teamToAdd.IsNorthAmerica)
            {
                if (IsNorthAmerica)
                {
                    return false;
                }
                _group.Add(teamToAdd);
            }
            else if (teamToAdd.IsSouthAmerica)
            {
                if (IsSouthAmerica)
                {
                    return false;
                }
                _group.Add(teamToAdd);
            }
            
        }

        private bool ProcessTeamAccToContinent(bool continent)
        {
            return false;
        }
        public static void ProcesssDroppedTeam(Team droppedTeam)
        {
            if(droppedTeam.Flag.Left == 0) return;   
        }
    }
}