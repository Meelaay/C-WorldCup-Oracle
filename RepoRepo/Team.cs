using System;
using System.Drawing;
using System.Windows.Forms;

namespace RepoRepo
{
    public class Team
    {
        public PictureBox Flag { get; set; }
        public string Name { get; set; }
        //todo add pot of every team? or maybe a bool that represent the continent

        public bool IsAfrica { get; set; }
        public bool IsAsia { get; set; }
        public bool IsEurope { get; set; }
        public bool IsNorthAmerica { get; set; }
        public bool IsSouthAmerica { get; set; }

        public Team(string path, string name)
        {
            Flag = new PictureBox {Image = Image.FromFile(path)};
            Flag.Size = new Size((int)(Flag.Image.Width / 1.25), (int)(Flag.Image.Height / 1.25));
            Flag.SizeMode = PictureBoxSizeMode.StretchImage;
            
            this.Name = name;
            //238, 125 is position of team 1 in group A

            this.Flag.Click += new EventHandler(pictureBox_Click);

        }

        public void SetFlagPosition(Point position)
        {
            Flag.Left = position.X;
            Flag.Top = position.Y;
        }







        //events:
        void pictureBox_Click(object sender, EventArgs e)
        {
            string test = Name;

        }
        public void Show()
        {
            Flag.Visible = true;
        }
        
        //todo create a ctor overload for future use 
        
    }
}