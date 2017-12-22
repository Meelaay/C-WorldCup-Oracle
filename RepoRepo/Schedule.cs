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

        public Button ValidationButton = new Button();

        private void ValidateClick(object sender, EventArgs e)
        {
            ValidationButton.Enabled = false;
            foreach (var match in _matchesList)
                match.MakeFieldsReadOnly();
        }

        public List<Match> GetMatches()
        {
           return _matchesList;
        }

        public Schedule()
        {
            ValidationButton.Click += ValidateClick;
            ValidationButton.Text = "Validate";
            ValidationButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        public void SetPositions()
        {
            for (int i = 0; i < 6; i++)
            {
                _matchesList[i].Team1.Flag.Location = _team1Positions[i];

                _matchesList[i].Team2.Flag.Location = _team2Positions[i];

                _matchesList[i].DateLabel.Location = _labelPositions[i];

                _matchesList[i].score1.Location = _team1Positions[i];
                _matchesList[i].score1.Left += _matchesList[i].Team1.Flag.Width + 5;


                _matchesList[i].score2.Location = _team2Positions[i];
                _matchesList[i].score2.Left -= 25;

                ValidationButton.Location = _labelPositions[5];
                ValidationButton.Top += 100;
                ValidationButton.Left += 50;

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
                position.Y += 40;
            }

            position.X += 500;
            position.Y = OFFSET_OF_FLAGS_FROM_TOP;
            
            //_team2Positions.Count
            for (int i = 0; i < 6; i++)
            {
                _team2Positions.Add(position);
                position.Y += 40;
            }
            int x = (_team1Positions[0].X + _team2Positions[0].X) / 2;
            int y = (_team1Positions[0].Y + _team2Positions[0].Y) / 2;

            Point midPoint = new Point(x, y+20);

            //_labelPositions.Count
            for (int j = 0; j < 6; j++)
            {
                _labelPositions.Add(midPoint);
                midPoint.Y += 40;
            }
            
        }


        public void SetUpScheduleForGroup(Group group)
        {
            List<BasicTeam> teamsList = BasicTeam.DeepCopyTeamsList(group.GetGroupTeams());

            _matchesList.Add(new Match((BasicTeam)teamsList[0], (BasicTeam)teamsList[1], _day1));
            _matchesList.Add(new Match((BasicTeam)teamsList[0], (BasicTeam)teamsList[2], _day1));

            _matchesList.Add(new Match((BasicTeam)teamsList[0], (BasicTeam)teamsList[3], _day2));
            _matchesList.Add(new Match((BasicTeam)teamsList[1], (BasicTeam)teamsList[2], _day2));

            _matchesList.Add(new Match((BasicTeam)teamsList[1], (BasicTeam)teamsList[3], _day3));
            _matchesList.Add(new Match((BasicTeam)teamsList[2], (BasicTeam)teamsList[3], _day3));
        }

        
    }
}