using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace RepoRepo
{

    public class Engine32Teams
    {

        public static readonly List<int> POTNUMBERS = new List<int> { 1, 2, 3, 4 } ;
        private const int NUMBEROFTEAMS = 32;
        public static PictureBox MENUBOX { get; set; }

        private static readonly DataBaseConnection Connector = new DataBaseConnection();
        private static readonly List<Point> InitialPositionsList = new List<Point>(NUMBEROFTEAMS);
        private static Button _validationButton;
        private static Form _form2;
        private static int _totalTeamsCount = 0;
        

        #region Groups (A->H) and Pots (1->4)
            private static List<Group> _groupsList = new List<Group>(capacity:8);
            private static List<Pot> _potsList = new List<Pot>(capacity:4);
        #endregion


        public Engine32Teams(Form form, Button validationButton)
        {
            //238, 125 is position of team 1 in group A
            FillInitialPositions();
            SetMenuBox();

            _form2 = form;                   //BUG throw away at the end if not needed
            _validationButton = validationButton;

            _groupsList.Add(new Group(new Point(220, 80), new Point(420, 320), new Point(238, 125)));
            _groupsList.Add(new Group(new Point(420, 80), new Point(620, 320), new Point(458, 125)));
            _groupsList.Add(new Group(new Point(620, 80), new Point(820, 320), new Point(678, 125)));
            _groupsList.Add(new Group(new Point(820, 80), new Point(1020, 320), new Point(898, 125)));

            _groupsList.Add(new Group(new Point(220, 323), new Point(420, 563), new Point(238, 368)));
            _groupsList.Add(new Group(new Point(420, 323), new Point(620, 563), new Point(458, 368)));
            _groupsList.Add(new Group(new Point(620, 323), new Point(820, 563), new Point(678, 368)));
            _groupsList.Add(new Group(new Point(820, 323), new Point(1020, 563), new Point(898, 368)));

            for (int i = 0; i < POTNUMBERS.Count; i++)
                _potsList.Add(new Pot(DataBaseConnection.FillPotFromDataBase(i+1,InitialPositionsList)));

        }

        public static void ValidateClicked()
        {
            DataBaseConnection.UpdateGroupForEachTeamInDataBase(_groupsList); 
            CalculateSchedules();               
            UpdateSchedulesInDataBase();
            //pop the second form here
            _validationButton.Enabled = false;
            InitializeFormOfSchedules();
            _form2.Show();
        }

        private static void InitializeFormOfSchedules()
        {
            Schedule.FillPositions();
        }

        private static void FillControlsOfForm2(List<Match> listOfMatches)
        {
            foreach (var match in listOfMatches)
            {
                _form2.Controls.Add(match.Team1.Flag);
                _form2.Controls.Add(match.Team2.Flag);
                _form2.Controls.Add(match.DateLabel);
            }
        }

        private static void RemoveControlsOfForm2()
        {
            int j = _form2.Controls.Count;

            for (int i = 0; i < j; i++)
            {
                Type typeOfControl = _form2.Controls[i].GetType();

                if (typeOfControl == typeof(PictureBox) || typeOfControl == typeof(Label))
                {
                    var a = _form2.Controls[i];
                    Convert.ChangeType(a, typeOfControl);
                    _form2.Controls.Remove(a);
                    j = _form2.Controls.Count;
                    i--;
                }
            }
        }

        public void ShowScheduleForGroup(Group group)
        {
            group.GetSchedule().SetPositions();
            RemoveControlsOfForm2();
            FillControlsOfForm2(group.GetSchedule().GetMatches());
            _form2.Refresh();
        }


        private static void CalculateSchedules()
        {
            foreach (var group in _groupsList)
                group.PlanMatches();
        }

        private static void UpdateSchedulesInDataBase()
        {
            foreach (var group in _groupsList)
                Connector.AddScheduleToDataBase(group.GetSchedule().GetMatches());
        }

        private static void CheckIfDrawn()
        {
            if (_totalTeamsCount == NUMBEROFTEAMS)
                _validationButton.Enabled = true;
        }

        public static void ProcessMovement(Team team, Point whereLeft)
        {
            if (PositionToGroupChar(whereLeft) == 'x') { team.MoveTeam(team.ReturnWhereLeftPoint()); return; }
            //call a created function that returns a bool valid position or not
            char groupChar = PositionToGroupChar(whereLeft);
            //can -| we return group to process directly here

            Group groupToProcess = CharToGroup(groupChar);

            if (groupToProcess.ProcesssDroppedTeam(team, groupChar))
            {
                team.MoveTeam(groupToProcess.PositionWhereToGo(team));
                ++_totalTeamsCount;
                RemoveTeamFromPot(team);
                team.ShowTeam();
                CheckIfDrawn();
            }
            else team.MoveTeam(team.ReturnWhereLeftPoint());

            //if it's valid for a certain group give it the right position and flip its picture
            //else give it _initPoint
        }

        private static void RemoveTeamFromPot(Team team)
        {
            _potsList[team.Pot-1].RemoveTeamFromPot(team);
        }

        public static Group CharToGroup(char groupChar)
        {
            int index = 'h' - groupChar + 1;
            return _groupsList[_groupsList.Count-index];
        }

        private static char PositionToGroupChar(Point whereLeft)
        {
            for (int i = 0; i < _groupsList.Count; i++)
            {
                if (Contains(whereLeft, _groupsList[i].BORDERPOINTS[0], _groupsList[i].BORDERPOINTS[2]))
                    return (char)('a' + i);
            }

            return 'x'; //<- 'x' means goes back to its place
        }

        private static bool Contains(Point belongs, Point p1, Point p3)
        {
            if (belongs.X <= p3.X && belongs.Y <= p3.Y)
                if (belongs.X >= p1.X && belongs.Y >= p1.Y)
                    return true;

            return false;
        }

        public List<PictureBox> GetPotPictureBoxes(int pot)
        {
            if (pot != 1 && pot != 2 && pot != 3 && pot != 4)
                throw new NullReferenceException("Engine32Teams::GetPotPictureBoxes() -> passed pot is invalid.");

            return _potsList[pot - 1].GetPictureBoxes();
        }

        private static void SetMenuBox()
        {
            Image menu = Image.FromFile(@"..\..\Sprites\main\menu1.png");
            MENUBOX = new PictureBox();
            MENUBOX.Size = new Size((int)(menu.Width / 1.25), (int)(menu.Height / 1.25));
            MENUBOX.Left = 200;
            MENUBOX.Top = 0;
            MENUBOX.Image = menu;
            MENUBOX.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        

        private static void FillInitialPositions()
        {
            const int OFFSET_OF_FLAGS_FROM_LEFT = 7;
            
            //BUG this was 0 and changed to fix offset -> results of this change are unkown
            const int OFFSET_OF_FLAGS_FROM_TOP = 25;


            Point position = new Point(OFFSET_OF_FLAGS_FROM_LEFT, OFFSET_OF_FLAGS_FROM_TOP);

            for (int i = 0; i <= NUMBEROFTEAMS; i++)
            {
                InitialPositionsList.Add(position);
                position.Y += 38;
                if (i == 15)
                {
                    position.X += 1119;
                    position.Y = OFFSET_OF_FLAGS_FROM_TOP;
                }
            }
        }


        
    }
}