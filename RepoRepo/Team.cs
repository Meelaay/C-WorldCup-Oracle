using System.Drawing;
using System.Windows.Forms;

namespace RepoRepo
{
    public class Team
    {
        PictureBox flag = new PictureBox();

        public PictureBox SetFlag(string path)
        {

            flag.Image = Image.FromFile(path);
            flag.Size = new Size(flag.Image.Width, flag.Image.Height);
            flag.Left = 0;
            flag.Top = 0;
            return flag;
        }

        public void Show()
        {
            flag.Visible = true;
        }
        
        //todo create a ctor overload for future use 
        
    }
}