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
        private DataBaseConnection _connector = new DataBaseConnection();
        private const int NUMBEROFTEAMS = 32;

        public const int POT1 = 1;
        public const int POT2 = 2;
        public const int POT3 = 3;
        public const int POT4 = 4;

        private List<Point> _initialPositionsList = new List<Point>(NUMBEROFTEAMS + 1);
        private List<string> _teamNames = new List<string>(NUMBEROFTEAMS + 1);
        public static PictureBox MENUBOX { get; set; }

        #region Groups (A->H)
            private Group _groupA;
            private Group _groupB;
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

        private Pot _pot1;
        private Pot _pot2;
        private Pot _pot3;
        private Pot _pot4;

        #endregion

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

        public Engine32Teams()
        {
            
            

            FillInitialPositions();

            _pot1 = new Pot(FillPotFromDataBase(POT1.ToString()));
            _pot2 = new Pot(FillPotFromDataBase(POT2.ToString()));
            _pot3 = new Pot(FillPotFromDataBase(POT3.ToString()));
            _pot4 = new Pot(FillPotFromDataBase(POT4.ToString()));

            SetMenuBox();

            //todo find a solution to positions of group without redundancy
            _groupA = new Group();
            _groupB = new Group();
                /*    
            _groupC = new Group();
            _groupD = new Group();
            _groupE = new Group();
            _groupF = new Group();
            _groupG = new Group();
            _groupH = new Group();
                */    
            }

        private void FillInitialPositions()
        {
            _initialPositionsList.Add(new Point(-1, -1));

            Point position = new Point(7, 0);

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

        private List<Team> FillPotFromDataBase(string pot)
        {
                                      //todo not * fix IT ** you can change it here if it requirements chage in future
            var teamsListOfPot = new List<Team>();
            string query = String.Format("SELECT country, pot FROM teams t WHERE t.pot = '{0}' ", pot);//pot is 1-2-3-4 not a-b-c-d
            DataTable dt = this._connector.ExecuteQuery(query);
            int potInteger = Convert.ToInt32(pot);
            int i = 0;
            switch (potInteger)
            {
                case 1:
                    i = 0;
                    break;
                case 2:
                    i = 7;
                    break;
                case 3:
                    i = 16;
                    break;
                case 4:
                    i = 23;
                    break;
            }
            
            foreach (DataRow row in dt.Rows)
            {
                var team = RowToTeam(row);
                team.SetFlagPosition(this._initialPositionsList[++i]);
                teamsListOfPot.Add(team);
            }

            if (teamsListOfPot.Count == 0) throw new NullReferenceException("FillPotFromDataBase -> teamsListOfPot is empty.");
            
            return teamsListOfPot;

        }

        private Team RowToTeam(DataRow row)
        {
            string name = row["COUNTRY"].ToString();
            string path = @"..\..\Sprites\teams\" + name + ".png";

            Team team = new Team(path, name);
            return team; 
        }


        
    }
}