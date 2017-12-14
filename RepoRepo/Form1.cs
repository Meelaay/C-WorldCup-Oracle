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
            /**
             * Ways to improve UI :
             *      -make background color red, black or something != white
             *      -change to all red spaces in group area
             *      -remove background from pieces 
             *      -change pictureBox to Pot1...
             */
            buttonValidate.Enabled = false;

            GlobalEngine.engine32 = new Engine32Teams(this, buttonValidate);
                        
            this.Size = new Size((int) (Engine32Teams.MENUBOX.Width * 1.25) + 200,
                                 (int) (Engine32Teams.MENUBOX.Height * 1.18)
                                );
            
            StartPosition = FormStartPosition.CenterScreen;

            this.Controls.Add(Engine32Teams.MENUBOX);

            foreach (var picture in GlobalEngine.engine32.GetPotPictureBoxes(Engine32Teams.POT1))
            {
                this.Controls.Add(picture);
            }
            foreach (var picture in GlobalEngine.engine32.GetPotPictureBoxes(Engine32Teams.POT2))
            {
                this.Controls.Add(picture);
            }
            foreach (var picture in GlobalEngine.engine32.GetPotPictureBoxes(Engine32Teams.POT3))
            {
                this.Controls.Add(picture);
            }
            foreach (var picture in GlobalEngine.engine32.GetPotPictureBoxes(Engine32Teams.POT4))
            {
                this.Controls.Add(picture);
            }


            this.pictureBox1.SendToBack();
            

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            

        }

        private Point GetPosition(Control c)
        {
            return c.FindForm().PointToClient(c.Parent.PointToScreen(c.Location));
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //manual implementation
        }

        private void automaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //automatic impl..
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            Engine32Teams.ValidateClicked();
            
            //pop the new form2 with groupA as main one then fix the other stuff of visibility and clicking
        }
    }
}
