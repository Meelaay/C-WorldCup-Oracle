using System;
using System.Collections.Generic;
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

        private static readonly List<Image> _potImages = new List<Image>
        {
            Image.FromFile(@"..\..\Sprites\pots\pot1.png"),
            Image.FromFile(@"..\..\Sprites\pots\pot2.png"),
            Image.FromFile(@"..\..\Sprites\pots\pot3.png"),
            Image.FromFile(@"..\..\Sprites\pots\pot4.png")
        };

        private Image _initialImage;

        private List<MouseEventHandler> _mouseEventHandlers = new List<MouseEventHandler>(capacity:3);
        // 0 -> mouseDown, 1 -> mouseMove, 2 -> mouseUp
        



        public Team(string path, string name, string continent, string pot)
            : base(null, name, continent, null)
        {
            IsAfrica = IsAsia = IsEurope = IsNorthAmerica = IsSouthAmerica = false;
            this.Pot = Convert.ToInt32(pot);
            InitContinentBools(continent);
            this._initialImage = Image.FromFile(path);
            this.Flag = new PictureBox {Image = Image.FromFile(path)};
            this.Flag.Size = new Size((int)(Flag.Image.Width / 1.25), (int)(Flag.Image.Height / 1.25));
            this.Flag.SizeMode = PictureBoxSizeMode.StretchImage;

            //todo adding events to every team (picbox of a team) add here events for drag and drop
            this._mouseEventHandlers.Add(flag_mouseDown);
            this._mouseEventHandlers.Add(flag_mouseMove);
            this._mouseEventHandlers.Add(flag_mouseUp);

            this.Flag.MouseDown += _mouseEventHandlers[0];
            this.Flag.MouseMove += _mouseEventHandlers[1];
            this.Flag.MouseUp += _mouseEventHandlers[2];
            this.Flag.BringToFront();
            HideTeam(); 
            //<-- BUG probably not right spot
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

        public void HideTeam()
        {
            Flag.Image = _potImages[Pot - 1];
        }

        
        public void ShowTeam()
        {
            Flag.Image = _initialImage;
        }

        public void RemoveEvents()
        {
            //todo check for null or exception
            Flag.MouseDown -= _mouseEventHandlers[0];
            Flag.MouseMove -= _mouseEventHandlers[1];
            Flag.MouseUp -= _mouseEventHandlers[2];
        }

        public void SetFlagPosition(Point position)
        {
            Flag.Left = position.X;
            Flag.Top = position.Y;
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
            a.BringToFront();
            //TODO on release freeze team in position
        }

        public void ImitateMouseUp(PictureBox picture)
        {
            flag_mouseUp(picture, null);
        }

        public Point ReturnWhereLeftPoint()
        {
            return _initialPoint;
        }

        
        //todo create a ctor overload for future use 
        
    }
}