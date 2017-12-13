using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.String;

namespace RepoRepo
{

    public class Engine32Teams
    {
        public const int POT1 = 1;
        public const int POT2 = 2;
        public const int POT3 = 3;
        public const int POT4 = 4;
        private static Button _validationButton;
        private static readonly DataBaseConnection _connector = new DataBaseConnection();
        private const int NUMBEROFTEAMS = 32;
        private static List<Point> _initialPositionsList = new List<Point>(NUMBEROFTEAMS);
        private Form _form;
        private static int _totalTeamsCount = 0;
        
        public static PictureBox MENUBOX { get; set; }

        #region Groups (A->H)
            private static Group _groupA;
            private static Group _groupB;
            private static Group _groupC;
            private static Group _groupD;
            private static Group _groupE;
            private static Group _groupF;
            private static Group _groupG;
            private static Group _groupH;
        #endregion

        #region Pots (1->4)
            private static Pot _pot1;
            private static Pot _pot2;
            private static Pot _pot3;
            private static Pot _pot4;
        #endregion
        

        public Engine32Teams(Form form, Button validationButton)
        {
            //238, 125 is position of team 1 in group A
            FillInitialPositions();
            SetMenuBox();
            _form = form;                   //BUG throw away at the end if not needed
            _validationButton = validationButton;
            _groupA = new Group(new Point(220, 80), new Point(420, 320), new Point(238, 125));
            _groupB = new Group(new Point(420, 80), new Point(620, 320), new Point(458, 125));
            _groupC = new Group(new Point(620, 80), new Point(820, 320), new Point(678, 125));
            _groupD = new Group(new Point(820, 80), new Point(1020, 320), new Point(898, 125));

            _groupE = new Group(new Point(220, 323), new Point(420, 563), new Point(238, 368));
            _groupF = new Group(new Point(420, 323), new Point(620, 563), new Point(458, 368));
            _groupG = new Group(new Point(620, 323), new Point(820, 563), new Point(678, 368));
            _groupH = new Group(new Point(820, 323), new Point(1020, 563), new Point(898, 368));
            
            _pot1 = new Pot(FillPotFromDataBase(POT1.ToString()));
            _pot2 = new Pot(FillPotFromDataBase(POT2.ToString()));
            _pot3 = new Pot(FillPotFromDataBase(POT3.ToString()));
            _pot4 = new Pot(FillPotFromDataBase(POT4.ToString()));

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
                //function that returns where it should go 1-2-3-4 POSITION points in group
                team.MoveTeam(groupToProcess.PositionWhereToGo(team));
                ++_totalTeamsCount;
                //POP FROM POT
                RemoveTeamFromPot(team);
                //check if working
                team.ShowTeam();
                //CHANGE STATE OF TEAM (GROUP)
                CheckIfDrawn();
            }
            else team.MoveTeam(team.ReturnWhereLeftPoint());


            //if it's valid for a certain group give it the right position and flip its picture
            //else give it _initPoint

            //call return where landed
        }

        public static void ValidateClicked()
        {
            UpdateGroupForEachTeamInDataBase();
        }

        //BUG to be transfered as static method to DataBaseConnection
        private static void UpdateGroupForEachTeamInDataBase()
        {
            foreach (var team in _groupA.GetGroupTeams())
                _connector.UpdateTeamInDataBase(team);
            foreach (var team in _groupB.GetGroupTeams())
                _connector.UpdateTeamInDataBase(team);
            foreach (var team in _groupC.GetGroupTeams())
                _connector.UpdateTeamInDataBase(team);
            foreach (var team in _groupD.GetGroupTeams())
                _connector.UpdateTeamInDataBase(team);
            foreach (var team in _groupE.GetGroupTeams())
                _connector.UpdateTeamInDataBase(team);
            foreach (var team in _groupF.GetGroupTeams())
                _connector.UpdateTeamInDataBase(team);
            foreach (var team in _groupG.GetGroupTeams())
                _connector.UpdateTeamInDataBase(team);
            foreach (var team in _groupH.GetGroupTeams())
                _connector.UpdateTeamInDataBase(team);
        }

        private static void ScheduleMatches()
        {
            
        }

        private static void CheckIfDrawn()
        {
            if (_totalTeamsCount == NUMBEROFTEAMS)
            {
                _validationButton.Enabled = true;
            }
        }

        private static void RemoveTeamFromPot(Team team)
        {
            switch (team.Pot)
            {
                case 1:
                    _pot1.RemoveTeamFromPot(team);
                    return;
                case 2:
                    _pot2.RemoveTeamFromPot(team);
                    return;
                case 3:
                    _pot3.RemoveTeamFromPot(team);
                    return;
                case 4:
                    _pot4.RemoveTeamFromPot(team);
                    return;
            }
        }

        private static Group CharToGroup(char groupChar)
        {
            switch (groupChar)
            {
                case 'a': return _groupA;
                case 'b': return _groupB;
                case 'c': return _groupC;
                case 'd': return _groupD;
                case 'e': return _groupE;
                case 'f': return _groupF;
                case 'g': return _groupG;
                case 'h': return _groupH;
            }
            throw new NullReferenceException("Engine32Teams::CharToGroup() -> invalid groupChar passed as arg");
        }

        private static char PositionToGroupChar(Point whereLeft)
        {
            if (Contains(whereLeft, _groupA.BORDERPOINT1, _groupA.BORDERPOINT3))
                return 'a';
            if (Contains(whereLeft, _groupB.BORDERPOINT1, _groupB.BORDERPOINT3))
                return 'b';
            if (Contains(whereLeft, _groupC.BORDERPOINT1, _groupC.BORDERPOINT3))
                return 'c';
            if (Contains(whereLeft, _groupD.BORDERPOINT1, _groupD.BORDERPOINT3))
                return 'd';
            if (Contains(whereLeft, _groupE.BORDERPOINT1, _groupE.BORDERPOINT3))
                return 'e';
            if (Contains(whereLeft, _groupF.BORDERPOINT1, _groupF.BORDERPOINT3))
                return 'f';
            if (Contains(whereLeft, _groupG.BORDERPOINT1, _groupG.BORDERPOINT3))
                return 'g';
            if (Contains(whereLeft, _groupH.BORDERPOINT1, _groupH.BORDERPOINT3))
                return 'h';

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
            switch (pot)
            {
                case 1: return _pot1.GetPictureBoxes();
                case 2: return _pot2.GetPictureBoxes();
                case 3: return _pot3.GetPictureBoxes();
                case 4: return _pot4.GetPictureBoxes();
            }

            throw new NullReferenceException("Engine32Teams::GetPotPictureBoxes() -> passed pot is invalid.");
        }

        private static void SetMenuBox()
        {
            Image menu = Image.FromFile(@"..\..\Sprites\main\menu1.png");
            MENUBOX = new PictureBox();
            MENUBOX.Size = new Size((int)(menu.Width / 1.25), (int)(menu.Height / 1.25));
            MENUBOX.Left = 200;
            //var a = _pot1.GetPictureBoxes();
            //MENUBOX.Left = (int)(a[0].Image.Width / 1.20);
            MENUBOX.Top = 0;
            MENUBOX.Image = menu;
            MENUBOX.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private static List<Team> FillPotFromDataBase(string pot)
        {
                                      //todo not * fix IT ** you can change it here if it requirements chage in future
            var teamsListOfPot = new List<Team>();
            string query = Format("SELECT country, pot, continent FROM teams t WHERE t.pot = '{0}' ", pot);//pot is 1-2-3-4 not a-b-c-d
            DataTable dt = _connector.ExecuteQuery(query);
            int potInteger = Convert.ToInt32(pot);
            int i = 0;
            if (potInteger == 1)      i = 0;
            else if (potInteger == 2) i = 8;
            else if (potInteger == 3) i = 16;
            else if (potInteger == 4) i = 24;
                        
            foreach (DataRow row in dt.Rows)
            {
                var team = DataBaseConnection.RowToTeam(row);
                team.SetFlagPosition(_initialPositionsList[i++]);
                teamsListOfPot.Add(team);
            }

            if (teamsListOfPot.Count == 0) throw new NullReferenceException("FillPotFromDataBase -> teamsListOfPot() is empty.");
            return teamsListOfPot;

        }

        private static void FillInitialPositions()
        {
            const int OFFSET_OF_FLAGS_FROM_LEFT = 7;
            
            //BUG this was 0 and changed to fix offset -> results of this change are unkown
            const int OFFSET_OF_FLAGS_FROM_TOP = 25;


            Point position = new Point(OFFSET_OF_FLAGS_FROM_LEFT, OFFSET_OF_FLAGS_FROM_TOP);

            for (int i = 0; i <= NUMBEROFTEAMS; i++)
            {
                _initialPositionsList.Add(position);

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