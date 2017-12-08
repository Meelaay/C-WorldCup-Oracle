using System.Drawing;
using System.Windows.Forms;

namespace RepoRepo
{
    public class Team
    {
        private readonly PictureBox _flag;

        public Team(string path, Point position)
        {
            _flag = new PictureBox();
            _flag.Image = Image.FromFile(path);
            _flag.Size = new Size((int)(_flag.Image.Width / 1.25), (int)(_flag.Image.Height / 1.25));
            _flag.SizeMode = PictureBoxSizeMode.StretchImage;
            _flag.Left = position.X;
            _flag.Top = position.Y;

            //238, 125 is position of team 1 in group A

        }
        public PictureBox GetFlag()
        {
            return _flag;
        }

        public void Show()
        {
            _flag.Visible = true;
        }
        
        //todo create a ctor overload for future use 
        
    }
}