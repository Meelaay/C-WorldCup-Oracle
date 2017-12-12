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
        private static DataBaseConnection _connector = new DataBaseConnection();
        private const int NUMBEROFTEAMS = 32;
        private Form _form;
        public const int POT1 = 1;
        public const int POT2 = 2;
        public const int POT3 = 3;
        public const int POT4 = 4;

        private static List<Point> _initialPositionsList = new List<Point>(NUMBEROFTEAMS + 1);

        private static char PositionToGroup(Point whereLeft)
        {
            //logic of ifs >= <= with border
            return 'a';
        }

        private static void FillBordersOfGroups()
        {
            //todo and hide
        }
        //for what??
        private List<string> _teamNames = new List<string>(NUMBEROFTEAMS + 1);

        public static PictureBox MENUBOX { get; set; }

        private Point test = new Point();

        void pictureBox_Click(object sender, MouseEventArgs e)
        {
            var b = e.Location;
            
            var test = (PictureBox)sender;
        }

        private static Point GetPosition(Control c)
        {
            return c.FindForm().PointToClient(c.Parent.PointToScreen(c.Location));
        }

        #region Groups (A->H)
        private static Group _groupA;
            private static Group _groupB;
        /*
            private Group _groupC;
            private Group _groupD;
            private Group _groupE;
            private Group _groupF;
            private Group _groupG;
            private Group _groupH;
        */
        #endregion

        #region Pots (1->4)

            private static Pot _pot1;
            private static Pot _pot2;
            private static Pot _pot3;
            private static Pot _pot4;

        #endregion
        
        public static void ProcessMovement(Team team, Point whereLeft)
        {
            //call a created function that returns a bool valid position or not
            char groupChar = PositionToGroup(whereLeft);
            //can -| we return group to process directly here
            switch (groupChar)
            {
                case 'x': return;
                    //create a function that selects right group from char and then returns group then process returned group
                    //this.CharToGroup(groupChar)
                case 'a':
                    if (_groupA.ProcesssDroppedTeam(team))
                    {
                        //238, 125 is position of team 1 in group A
                        //function that returns where it should go 1-2-3-4 points in group
                        team.MoveTeam(_groupA.PositionWhereToGo(team));//for the moment -> change to right position later
                    }
                    
                    break;
            }
            //if it's valid for a certain group give it the right position and flip its picture
            //else give it _initPoint

            //call return where landed
        }


        public List<PictureBox> GetPotPictureBoxes(int pot)
        {
            switch (pot)
            {
                case 1:
                    return _pot1.GetPictureBoxes();
                    break;
                case 2:
                    return _pot2.GetPictureBoxes();
                    break;
                case 3:
                    return _pot3.GetPictureBoxes();
                    break;
                case 4:
                    return _pot4.GetPictureBoxes();
                    break;
            }
            throw new NullReferenceException("GetPotPictureBoxes -> passed pot is invalid.");

        }

        

        public Engine32Teams(Form form)
        {
            _groupA = new Group(new Point(220, 80), new Point(420, 80), new Point(220, 320), new Point(420, 320));
            
            _form = form;
            FillInitialPositions();
            FillBordersOfGroups();
            _pot1 = new Pot(FillPotFromDataBase(POT1.ToString()));
            _pot2 = new Pot(FillPotFromDataBase(POT2.ToString()));
            _pot3 = new Pot(FillPotFromDataBase(POT3.ToString()));
            _pot4 = new Pot(FillPotFromDataBase(POT4.ToString()));

            SetMenuBox();
            MENUBOX.MouseDown += pictureBox_Click;
            //todo find a solution to positions of group without redundancy
            // _groupA = new Group();
            // _groupB = new Group();
            /*    
        _groupC = new Group();
        _groupD = new Group();
        _groupE = new Group();
        _groupF = new Group();
        _groupG = new Group();
        _groupH = new Group();
            */
        }


        private void SetMenuBox()
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
                var team = RowToTeam(row);
                team.SetFlagPosition(_initialPositionsList[++i]);
                teamsListOfPot.Add(team);
            }

            if (teamsListOfPot.Count == 0) throw new NullReferenceException("FillPotFromDataBase -> teamsListOfPot() is empty.");
            return teamsListOfPot;

        }

        /// <summary>
        /// should go to database connection as a public static
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static Team RowToTeam(DataRow row)
        {
            string name = row["COUNTRY"].ToString();
            string continent = row["CONTINENT"].ToString();
            string pot = row["POT"].ToString();

            string path = @"..\..\Sprites\teams\" + name + ".png";

            Team team = new Team(path, name, continent, pot);
            if (IsNullOrWhiteSpace(path) || IsNullOrWhiteSpace(name)) throw new NullReferenceException("RowToTeam() -> path or name is empty.");
            {
                
            }
            return team; 
        }
        private void FillInitialPositions()
        {
            _initialPositionsList.Add(new Point(-1, -1));
            const int OFFSET_OF_FLAGS_FROM_LEFT = 7;
            const int OFFSET_OF_FLAGS_FROM_TOP = 0;

            Point position = new Point(OFFSET_OF_FLAGS_FROM_LEFT, OFFSET_OF_FLAGS_FROM_TOP);

            for (int i = 1; i <= NUMBEROFTEAMS; i++)
            {
                _initialPositionsList.Add(position);

                position.Y += 38;

                if (i == 16)
                {
                    position.X += 1119;
                    position.Y = 0;
                }
            }
        }


    }
}