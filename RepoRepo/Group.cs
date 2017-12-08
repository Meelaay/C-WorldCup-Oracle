using System.Collections.Generic;
using System.Drawing;

namespace RepoRepo
{
    public class Group
    {
        private List<Team> _group = new List<Team>(capacity: 5);
        

        public Group()
        {
            
        }

        public Group(List<Point> positionsOf)
        {
            _group.Add(null);
            _group.Add(null);
        }
    }
}