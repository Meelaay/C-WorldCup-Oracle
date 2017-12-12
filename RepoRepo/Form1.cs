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

            engine32 = new Engine32Teams(this);
                        
            this.Size = new Size((int) (Engine32Teams.MENUBOX.Width * 1.25) + 200,
                                 (int) (Engine32Teams.MENUBOX.Height * 1.25)
                                );
            
            StartPosition = FormStartPosition.CenterScreen;

            this.Controls.Add(Engine32Teams.MENUBOX);

            foreach (var picture in engine32.GetPotPictureBoxes(Engine32Teams.POT1))
            {
                this.Controls.Add(picture);
            }
            foreach (var picture in engine32.GetPotPictureBoxes(Engine32Teams.POT2))
            {
                this.Controls.Add(picture);
            }
            foreach (var picture in engine32.GetPotPictureBoxes(Engine32Teams.POT3))
            {
                this.Controls.Add(picture);
            }
            foreach (var picture in engine32.GetPotPictureBoxes(Engine32Teams.POT4))
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
