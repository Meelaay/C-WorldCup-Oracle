using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
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
            private static readonly List<Group> GroupsList = new List<Group>(capacity:8);
            private static readonly List<Pot> PotsList = new List<Pot>(capacity:4);
        #endregion


        public Engine32Teams(Form form, Button validationButton)
        {
            //238, 125 is position of team 1 in group A
            FillInitialPositions();
            SetMenuBox();

            _form2 = form;                   //BUG throw away at the end if not needed
            _validationButton = validationButton;

            GroupsList.Add(new Group(new Point(220, 80), new Point(420, 320), new Point(238, 125), 'a'));
            GroupsList.Add(new Group(new Point(420, 80), new Point(620, 320), new Point(458, 125), 'b'));
            GroupsList.Add(new Group(new Point(620, 80), new Point(820, 320), new Point(678, 125), 'c'));
            GroupsList.Add(new Group(new Point(820, 80), new Point(1020, 320), new Point(898, 125), 'd'));

            GroupsList.Add(new Group(new Point(220, 323), new Point(420, 563), new Point(238, 368), 'e'));
            GroupsList.Add(new Group(new Point(420, 323), new Point(620, 563), new Point(458, 368), 'f'));
            GroupsList.Add(new Group(new Point(620, 323), new Point(820, 563), new Point(678, 368), 'g'));
            GroupsList.Add(new Group(new Point(820, 323), new Point(1020, 563), new Point(898, 368), 'h'));

            for (int i = 0; i < POTNUMBERS.Count; i++)
                PotsList.Add(new Pot(DataBaseConnection.FillPotFromDataBase(i + 1, InitialPositionsList)));

            // MOVING RUSSIA TO 1 GROUP A
            //called in form1.cs for bug in here

        }

        public static void FinalizeEngineToTree()
        {
            List<List<string>> champions = Connector.ChampionsOfGroup(GroupsList);

            StringBuilder championsBuilder = new StringBuilder();



            foreach (var team in champions[0])
                championsBuilder.Append(team + " ");

            for (int i = 0; i < 8; i+=2)
            {
                int index = i;
                int nextIndex = i+1;

                championsBuilder.Append(champions[1][nextIndex] + " ");
                championsBuilder.Append(champions[1][index] + " ");
            }
            foreach (var team in champions[1])
                championsBuilder.Append(team + " ");

            string cParamss = championsBuilder.ToString();
            string cParams = String.Copy(cParamss);
            string cPath = @".\arbre32.exe";

            System.Diagnostics.Process.Start(cPath, cParams);
        }

        
        public static void UpdateMatchResults(List<Match> matchesList)
        {
            Connector.UpdateMatchesInDataBase(matchesList);
        }

        public static void UpdateTeamsStats(List<BasicTeam> finalTeams)
        {
            Connector.UpdateTeamsStats(finalTeams);
        }

        public static void ShowAndFixPosition()
        {
            foreach (var pot in PotsList)
                pot.ShowPot();
        }

        public static void Hide()
        {
            foreach (var pot in PotsList)
                pot.HidePot();
        }


        public static void RussiaToA()
        {
            MoveTeamToGroup(PotsList[0]._potTeams[2][0], 'a');
        }

        private static void RandomizeTeamsOfContinentOnPot(ref int i, int potNumber, string continent)
        {
            while (PotsList[potNumber-1].ContainsContinent(continent))
            {
                var randomTeam = PotsList[potNumber-1].GetRandomTeamFromSubSet(continent);
                if (GroupsList[i].IsValid(randomTeam))
                    MoveTeamToGroup(randomTeam, GroupsList[i]._groupChar);
                i++;
            }
            i = 0;
        }

        public static void RandomizePot1()
        {
            int i = 0;
            while (PotsList[0].GetTotalNumberOfTeams() != 0)
            {
                RandomizeTeamsOfContinentOnPot(ref i, POTNUMBERS[0], "europe");
                RandomizeTeamsOfContinentOnPot(ref i, POTNUMBERS[0], "southamerica");
            }
        }

        public static void RandomizePot2()
        {
            int i = 0;
            while (PotsList[1].GetTotalNumberOfTeams() != 0)
            {
                RandomizeTeamsOfContinentOnPot(ref i, POTNUMBERS[1], "southamerica");
                RandomizeTeamsOfContinentOnPot(ref i, POTNUMBERS[1], "northamerica");
                RandomizeTeamsOfContinentOnPot(ref i, POTNUMBERS[1], "europe");
            }
        }

        public static void RandomizePot3()
        {
            int i = 0;
            while (PotsList[2].GetTotalNumberOfTeams() != 0)
            {
                RandomizeTeamsOfContinentOnPot(ref i, POTNUMBERS[2], "northamerica");
                RandomizeTeamsOfContinentOnPot(ref i, POTNUMBERS[2], "europe");
                RandomizeTeamsOfContinentOnPot(ref i, POTNUMBERS[2], "africa");
                RandomizeTeamsOfContinentOnPot(ref i, POTNUMBERS[2], "asia");
            }
        }

        public static void RandomizePot4()
        {
            int i = 0;
            while (PotsList[3].GetTotalNumberOfTeams() != 0)
            {
                RandomizeTeamsOfContinentOnPot(ref i, POTNUMBERS[3], "europe");
                RandomizeTeamsOfContinentOnPot(ref i, POTNUMBERS[3], "africa");
                RandomizeTeamsOfContinentOnPot(ref i, POTNUMBERS[3], "asia");
                RandomizeTeamsOfContinentOnPot(ref i, POTNUMBERS[3], "northamerica");
            }

        }
        private static void MoveTeamToGroup(Team team, char groupChar)
        {
            var group = CharToGroup(groupChar);
            team.Flag.Location = new Point(group.TEAMPOSITIONS[0].X + 5, group.TEAMPOSITIONS[0].Y + 5);
            team.ImitateMouseUp(team.Flag);
            team.Flag.BringToFront();
            team.ShowTeam();
        }

        public static void ValidateClicked()
        {
            DataBaseConnection.UpdateGroupForEachTeamInDataBase(GroupsList); 
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

        private static void FillControlsOfForm2(List<Match> listOfMatches, Button validationButton)
        {
            foreach (var match in listOfMatches)
            {
                _form2.Controls.Add(match.Team1.Flag);
                _form2.Controls.Add(match.Team2.Flag);
                _form2.Controls.Add(match.DateLabel);
                _form2.Controls.Add(match.score1);
                _form2.Controls.Add(match.score2);
            }

            _form2.Controls.Add(validationButton);
        }

        private static void RemoveControlsOfForm2()
        {
            int j = _form2.Controls.Count;

            for (int i = 0; i < j; i++)
            {
                Type typeOfControl = _form2.Controls[i].GetType();

                if (typeOfControl == typeof(PictureBox) || typeOfControl == typeof(Label) 
                    || typeOfControl == typeof(TextBox) || typeOfControl == typeof(Button))
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
            FillControlsOfForm2(group.GetSchedule().GetMatches(), group.GetButtonFromSchedule());
            _form2.Refresh();
        }


        private static void CalculateSchedules()
        {
            foreach (var group in GroupsList)
                group.PlanMatches();
        }

        private static void UpdateSchedulesInDataBase()
        {
            foreach (var group in GroupsList)
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

            if (groupToProcess.ProcesssDroppedTeam(team))
            {
                team.MoveTeam(groupToProcess.PositionWhereToGo(team));
                ++_totalTeamsCount;
                RemoveTeamFromPot(team);
                team.ShowTeam();
                CheckIfDrawn();
            }
            else
                team.MoveTeam(team.ReturnWhereLeftPoint());

            //if it's valid for a certain group give it the right position and flip its picture
            //else give it _initPoint
        }

        private static void RemoveTeamFromPot(Team team)
        {
            PotsList[team.Pot-1].RemoveTeamFromPot(team);
        }

        public static Group CharToGroup(char groupChar)
        {
            int index = 'h' - groupChar + 1;
            return GroupsList[GroupsList.Count-index];
        }

        private static char PositionToGroupChar(Point whereLeft)
        {
            for (int i = 0; i < GroupsList.Count; i++)
                if (Contains(whereLeft, GroupsList[i].BORDERPOINTS[0], GroupsList[i].BORDERPOINTS[2]))
                    return (char)('a' + i);

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

            return PotsList[pot - 1].GetPictureBoxes();
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