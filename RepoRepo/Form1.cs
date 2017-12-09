using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
namespace RepoRepo
{
    public partial class Form1 : Form
    {
        private Engine32Teams engine32;

        public Form1()
        {
            InitializeComponent();

            engine32 = new Engine32Teams();

            Image menu = Image.FromFile(@"..\..\Sprites\main\menu1.png");
            pictureBox1.Size = new Size((int) (menu.Width / 1.25), (int) (menu.Height / 1.25));

            pictureBox1.Left = 200;
            pictureBox1.Top = 0;
            pictureBox1.Image = menu;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            this.Size = new Size(menu.Width+200, menu.Height);
            
            StartPosition = FormStartPosition.CenterScreen;

            foreach (var picture in engine32.GetPotPictureBoxes(Engine32Teams.POT1))
            {
                this.Controls.Add(picture);
            }
            foreach (var picture in engine32.GetPotPictureBoxes(Engine32Teams.POT2))
            {
                this.Controls.Add(picture);
            }


            this.pictureBox1.SendToBack();
            

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Point buttonPosition = new Point(0, 0);

            debugTextBox.Text = GetPosition(pictureBox1).ToString();
            debugTextBox.AppendText(e.Location.ToString());

        }

        private Point GetPosition(Control c)
        {
            return c.FindForm().PointToClient(c.Parent.PointToScreen(c.Location));
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            debugTextBox.Text = e.Location.ToString();
        }

        /*
        private void button1_Click(object sender, EventArgs e)
        {
            DataBaseConnection a = new DataBaseConnection();
            a.EstablishConnection();
        }*/
    }
}
