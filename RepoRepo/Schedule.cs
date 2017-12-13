using System;
using System.Collections.Generic;
using System.Drawing;

namespace RepoRepo
{
    public class Schedule
    {
        private List<Match> _matchesList = new List<Match>(capacity:6);

        private List<Point> _positions = new List<Point>(capacity:12);


        private void SetUpScheduleForGroup(Group group)
        {
            DateTime day1 = new DateTime(2017, 12, 25);
            DateTime day2 = new DateTime(2017, 12, 26); 
            DateTime day3 = new DateTime(2017, 12, 27);
            
            List<Team> teamsList = group.GetGroupTeams();
            _matchesList.Add(new Match(teamsList[0], teamsList[1], day1));
            _matchesList.Add(new Match(teamsList[0], teamsList[1], day1));
            _matchesList.Add(new Match(teamsList[0], teamsList[1], day1));
            _matchesList.Add(new Match(teamsList[0], teamsList[1], day1));
            _matchesList.Add(new Match(teamsList[0], teamsList[1], day1));
            _matchesList.Add(new Match(teamsList[0], teamsList[1], day1));
        }

        public Schedule(Group group)
        {
            
        }      
    }
}