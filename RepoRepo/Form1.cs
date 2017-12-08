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
        
        public Form1()
        {
            InitializeComponent();
            Image menu = Image.FromFile(@"..\..\Sprites\main\menu1.png");
            pictureBox1.Size = new Size(menu.Width, menu.Height); //1150 x 715
            Size = new Size(menu.Width + 400, menu.Height);
            StartPosition = FormStartPosition.CenterScreen;
            pictureBox1.Left = 200;
            pictureBox1.Top = 0;
            pictureBox1.Image = menu;
                    
            Team team1 = new Team();
            this.Controls.Add(team1.SetFlag(@"..\..\Sprites\teams\argentina.png"));
            team1.Show();

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Point buttonPosition = new Point(0, 0);

            debugTextBox.Text = e.Location.ToString();
            
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
