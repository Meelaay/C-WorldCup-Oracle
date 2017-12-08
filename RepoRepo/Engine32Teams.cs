using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RepoRepo
{
    public class Engine32Teams
    {
        
        private const int NUMBEROFTEAMS = 32;

        private List<Point> _initialPositionsList = new List<Point>(NUMBEROFTEAMS + 1);

        private void FillInitialPositions()
        {
            _initialPositionsList.Add(new Point(-1, -1));

            Point position = new Point(0, 0);

            for (int i = 1; i <= NUMBEROFTEAMS/2; i++)
            {
                _initialPositionsList.Add(position);
                position.Y += 41 + 3;
            }
            
        }

        #region Groups (A->H)
            private Group _groupA;
            private Group _groupB;
            private Group _groupC;
            private Group _groupD;
            private Group _groupE;
            private Group _groupF;
            private Group _groupG;
            private Group _groupH;
        #endregion

        #region Pots (1->4)
            


        #endregion


        public Engine32Teams()
        {
            FillInitialPositions();
            _groupA = new Group();
            _groupB = new Group();
            _groupC = new Group();
            _groupD = new Group();
            _groupE = new Group();
            _groupF = new Group();
            _groupG = new Group();
            _groupH = new Group();
        }

    }
}