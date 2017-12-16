using System;
using System.Drawing;
using System.Windows.Forms;

namespace RepoRepo
{
    
    public class Team : BasicTeam
    {

        //todo add bools that represent group and make function that sets them to call in ctor 
        //todo same for bool pots
        public int Pot { get; set; }

        private Point _mouseDownLocation = new Point(0,0);
        private Point _initialPoint = new Point(0, 0);
        private Point _whereLeftPoint = new Point(0, 0);

        //todo add pot of every team? or maybe a bool that represent the continent

        #region Continent Bools
        public bool IsAfrica { get; set; }
        public bool IsAsia { get; set; }
        public bool IsEurope { get; set; }
        public bool IsNorthAmerica { get; set; }
        public bool IsSouthAmerica { get; set; }
        #endregion

        private static readonly Image _pot1Image = Image.FromFile(@"..\..\Sprites\pots\pot1.png");
        private static readonly Image _pot2Image = Image.FromFile(@"..\..\Sprites\pots\pot2.png");
        private static readonly Image _pot3Image = Image.FromFile(@"..\..\Sprites\pots\pot3.png");
        private static readonly Image _pot4Image = Image.FromFile(@"..\..\Sprites\pots\pot4.png");
        private Image _initialImage;
        private MouseEventHandler a ;
        private MouseEventHandler b ;
        private MouseEventHandler c ;



        public Team(string path, string name, string continent, string pot)
            : base(null, name, continent, null)
        {
            IsAfrica = IsAsia = IsEurope = IsNorthAmerica = IsSouthAmerica = false;
            this.Continent = continent;
            this.Pot = Convert.ToInt32(pot);
            this.InitContinentBools(continent);
            this.Name = name;
            _initialImage = Image.FromFile(path);
            this.Flag = new PictureBox {Image = Image.FromFile(path)};
            this.Flag.Size = new Size((int)(Flag.Image.Width / 1.25), (int)(Flag.Image.Height / 1.25));
            this.Flag.SizeMode = PictureBoxSizeMode.StretchImage;

            //todo adding events to every team (picbox of a team) add here events for drag and drop
            a = new MouseEventHandler(flag_mouseDown);
            b = new MouseEventHandler(flag_mouseMove);
            c = new MouseEventHandler(flag_mouseUp);

            this.Flag.MouseDown += a;
            this.Flag.MouseMove += b;
            this.Flag.MouseUp += c;

            HideTeam(); //<-- BUG probably not right spot
        }

        public void HideTeam()
        {

            switch (Pot)
            {
                case 1: Flag.Image = _pot1Image; return;
                case 2: Flag.Image = _pot2Image; return;
                case 3: Flag.Image = _pot3Image; return;
                case 4: Flag.Image = _pot4Image; return;
            }
        }

        
        public void ShowTeam()
        {
            Flag.Image = _initialImage;
        }

        public void RemoveEvents()
        {
            //todo check for null or exception
            Flag.MouseDown -= a;
            Flag.MouseMove -= b;
            Flag.MouseUp -= c;
        }



        private void InitContinentBools(string continent)
        {
            switch (continent)
            {
                case "africa":
                    this.IsAfrica = true;
                    return;
                case "asia":
                    this.IsAsia = true;
                    return;
                case "europe":
                    this.IsEurope = true;
                    return;
                case "northamerica":
                    this.IsNorthAmerica = true;
                    return;
                case "southamerica":
                    this.IsSouthAmerica = true;
                    return;
            }
            throw new Exception("InitContinentBools() -> invalid continent check passed arg.");
        }

        

        public void SetFlagPosition(Point position)
        {
            Flag.Left = position.X;
            Flag.Top = position.Y;
        }

        //As static function in engine
        private Point GetPosition(Control c)
        {
            return c.FindForm().PointToClient(c.Parent.PointToScreen(c.Location));
        }


        private void flag_mouseDown(object sender, MouseEventArgs e)
        {
            var a = (PictureBox) sender;
            _initialPoint = GetPosition(a);
            if (e.Button == MouseButtons.Left)
                _mouseDownLocation = e.Location;
            a.BringToFront();   
        }
        private void flag_mouseMove(object sender, MouseEventArgs e)
        {
            var a = (PictureBox) sender;
            if (e.Button == MouseButtons.Left)
            {
                a.Left = e.X + a.Left - _mouseDownLocation.X;
                a.Top = e.Y + a.Top - _mouseDownLocation.Y;
            }
        }

        private void flag_mouseUp(object sender, MouseEventArgs e)
        {
            var a = (PictureBox) sender;
            this._whereLeftPoint = a.Location;
            a.Location = _initialPoint;
            
            Engine32Teams.ProcessMovement(this, _whereLeftPoint);

            //TODO on release freeze team in position
        }

        public void MoveTeam(Point destination)
        {
            Flag.Left = destination.X;
            Flag.Top = destination.Y;
        }

        public Point ReturnWhereLeftPoint()
        {
            return _initialPoint;
        }

        //events:
        
        void pictureBox_Click(object sender, EventArgs e)
        {
            string test = Continent;

        }
        public void Show()
        {
            Flag.Visible = true;
        }
        
        //todo create a ctor overload for future use 
        
    }
}