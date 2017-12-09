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

        public Group(Point border1, Point border2, Point border3, Point border4)
        {
            BORDERPOINT1 = border1;
            BORDERPOINT2 = border2;
            BORDERPOINT3 = border3;
            BORDERPOINT4 = border4;
        }

        public bool AddTeamToGroup(Team teamToAdd)
        {
            //logic to add or not the team
            _group.Add(null);
            return false;
        }

        public static void ProcesssDroppedTeam(Team droppedTeam)
        {
         if(droppedTeam.Flag.Left == 0) return;   
        }
    }
}