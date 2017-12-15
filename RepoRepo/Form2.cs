using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RepoRepo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void groupAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            GlobalEngine.engine32.ShowScheduleForGroup(Engine32Teams.CharToGroup('a'));
        }

        private void groupBToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void groupCToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void groupDToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
