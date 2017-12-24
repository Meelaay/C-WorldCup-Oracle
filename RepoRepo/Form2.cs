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
            GlobalEngine.engine32.ShowScheduleForGroup(Engine32Teams.CharToGroup('b'));
        }

        private void groupCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalEngine.engine32.ShowScheduleForGroup(Engine32Teams.CharToGroup('c'));
        }

        private void groupDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalEngine.engine32.ShowScheduleForGroup(Engine32Teams.CharToGroup('d'));
        }

        private void groupEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalEngine.engine32.ShowScheduleForGroup(Engine32Teams.CharToGroup('e'));
        }

        private void groupFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalEngine.engine32.ShowScheduleForGroup(Engine32Teams.CharToGroup('f'));
        }

        private void groupGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalEngine.engine32.ShowScheduleForGroup(Engine32Teams.CharToGroup('g'));
        }

        private void groupHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalEngine.engine32.ShowScheduleForGroup(Engine32Teams.CharToGroup('h'));
        }

        private void passToTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine32Teams.FinalizeEngineToTree();
        }
    }
}
