using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RepoRepo
{
    public class Schedule
    {
        private List<Match> _matchesList = new List<Match>(capacity:6);

        private static List<Point> _team1Positions = new List<Point>(capacity: 6);
        private static List<Point> _team2Positions = new List<Point>(capacity: 6);

        private static List<Point> _labelPositions = new List<Point>(capacity: 6);//TODO fix this initialize it

        public static PictureBox DeepCopy(PictureBox pb)
        {
            return new PictureBox { Name = pb.Name, Image = pb.Image, Size = pb.Size, SizeMode = pb.SizeMode };
        }

        public List<Match> GetMatches()
        {
           return _matchesList;
        }

        public Schedule()
        {
            
        }

        public void SetPositions()
        {
            Match match;

            for (int i = 0; i < 6; i++)
            {
                match = _matchesList[i];
                match.Team1.Flag.Location = _team1Positions[i];
                match.Team2.Flag.Location = _team2Positions[i];
                match.DateLabel.Location = _labelPositions[i];
            }
        }


        private static readonly DateTime _day1 = new DateTime(2017, 12, 25);
        private static readonly DateTime _day2 = new DateTime(2017, 12, 26);
        private static readonly DateTime _day3 = new DateTime(2017, 12, 27);

        public static void FillPositions()
        {
            const int OFFSET_OF_FLAGS_FROM_LEFT = 10;

            //BUG this was 0 and changed to fix offset -> results of this change are unkown
            const int OFFSET_OF_FLAGS_FROM_TOP = 30;

            Point position = new Point(OFFSET_OF_FLAGS_FROM_LEFT, OFFSET_OF_FLAGS_FROM_TOP);

            //_team1Positions.Count
            for (int i = 0; i < 6; i++)
            {
                _team1Positions.Add(position);
                position.Y += 15;
            }

            position.X += 100;
            position.Y = OFFSET_OF_FLAGS_FROM_TOP;
            
            //_team2Positions.Count
            for (int i = 0; i < 6; i++)
            {
                _team2Positions.Add(position);
                position.Y += 50;
            }
            int x = (_team1Positions[0].X + _team2Positions[0].X) / 2;
            int y = (_team1Positions[0].Y + _team2Positions[0].Y) / 2;

            Point midPoint = new Point(x, y+600);

            //_labelPositions.Count
            for (int j = 0; j < 6; j++)
            {
                _labelPositions.Add(midPoint);
                midPoint.Y += 50;
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