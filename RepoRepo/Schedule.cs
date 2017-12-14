using System;
using System.Collections.Generic;
using System.Drawing;

namespace RepoRepo
{
    public class Schedule
    {
        private List<Match> _matchesList = new List<Match>(capacity:6);

        private static List<Point> _team1Positions = new List<Point>(capacity: 6);
        private static List<Point> _team2Positions = new List<Point>(capacity: 6);

        private static List<Point> _labelPositions = new List<Point>(capacity: 12);//TODO fix this initialize it

        public List<Match> GetMatches()
        {
           return _matchesList;
        }

        public Schedule()
        {
            FillPositions();
            //todo and fill label positions
        }

        private static readonly DateTime _day1 = new DateTime(2017, 12, 25);
        private static readonly DateTime _day2 = new DateTime(2017, 12, 26);
        private static readonly DateTime _day3 = new DateTime(2017, 12, 27);

        private void FillPositions()
        {
            const int OFFSET_OF_FLAGS_FROM_LEFT = 7;

            //BUG this was 0 and changed to fix offset -> results of this change are unkown
            const int OFFSET_OF_FLAGS_FROM_TOP = 25;

            Point position = new Point(OFFSET_OF_FLAGS_FROM_LEFT, OFFSET_OF_FLAGS_FROM_TOP);

            for (int i = 0; i < _team1Positions.Count; i++)
            {
                _team1Positions.Add(position);
                position.Y += 15;
            }

            position.X += 100;
            position.Y = OFFSET_OF_FLAGS_FROM_TOP;

            for (int i = 0; i < _team2Positions.Count; i++)
            {
                _team2Positions.Add(position);
                position.Y += 15;
            }

            
        }


        public void SetUpScheduleForGroup(Group group)
        {
            List<Team> teamsList = group.GetGroupTeams();

            _matchesList.Add(new Match(teamsList[0], teamsList[1], _day1));
            _matchesList.Add(new Match(teamsList[0], teamsList[2], _day1));

            _matchesList.Add(new Match(teamsList[0], teamsList[3], _day2));
            _matchesList.Add(new Match(teamsList[1], teamsList[2], _day2));

            _matchesList.Add(new Match(teamsList[1], teamsList[3], _day3));
            _matchesList.Add(new Match(teamsList[2], teamsList[3], _day3));
        }

        
    }
}