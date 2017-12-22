using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RepoRepo
{
    public class Group
    {
        public char _groupChar;

        private List<Team> _groupTeams = new List<Team>();

        private Schedule _schedule = new Schedule();

        //todo group should it know about pots logic ? or just stuff inside itself ? 
        //todo is it the engine property to manage right team from right pot to the right group ? so should it have the logic ?

        public Button GetButtonFromSchedule()
        {
            return _schedule.ValidationButton;
        }

        public readonly List<Point> BORDERPOINTS = new List<Point>();
        public readonly List<Point> TEAMPOSITIONS = new List<Point>();

        public Schedule GetSchedule()
        {
            return _schedule;
        }

        public List<Team> GetGroupTeams() //=> _group
        {
            return _groupTeams;
        }

        public bool IsComplete { get; set; } //could be substituted with count of list == 5

        public bool ContainsAfrica { get; set; }
        public bool ContainsAsia { get; set; }
        public bool ContainsEuropeFirst { get; set; }
        public bool ContainsEuropeSecond { get; set; }
        public bool ContainsNorthAmerica { get; set; }
        public bool ContainsSouthAmerica { get; set; }

        public List<bool> ContainsPot = new List<bool> {false, false, false, false};
        
        
        public Group(Point p1, Point p3, Point firstElement, char groupChar)
        {
            _groupChar = groupChar;
            FillBordersOfGroup(p1, p3);
            FillTeamPositionsInGroup(firstElement);

            ContainsAfrica = ContainsEuropeFirst = ContainsAsia = ContainsEuropeSecond = ContainsNorthAmerica = ContainsSouthAmerica = false;

            //BUG list was 1 indexed
            //_group.Add(null);


        }

        public void PlanMatches()
        {
            _schedule.SetUpScheduleForGroup(this);
        }

        private void FillBordersOfGroup(Point p1, Point p3)
        {
            //todo and hide : automates filling of group borders
            Point p2 = new Point(p3.X, p1.Y);
            Point p4 = new Point(p1.X, p3.Y);

            BORDERPOINTS.Add(p1);
            BORDERPOINTS.Add(p2);
            BORDERPOINTS.Add(p3);
            BORDERPOINTS.Add(p4);
        }

        private void FillTeamPositionsInGroup(Point firstElement)
        {
            //pass the first one as arg and then += y
            int x = firstElement.X; int y = firstElement.Y;

            for (int i = 0; i < 4; i++)
            {
                TEAMPOSITIONS.Add(new Point(x, y));
                y += 42;
            }
            
        }


        

        //todo put GetPosition as a static func in ENGINE
        public bool IsValid(Team team)
        {
            return !IsGroupComplete() && (IsValidForInsertionPotWise(team) && IsValidForInsertionContinentWise(team));
        }


        public bool ProcesssDroppedTeam(Team droppedTeam)
        {
            //todo method if you could SET BOOLS IN CTOR TO FALSE OR RIGHT THING FIX YOUR STUFF
            
            //AddTeam(droppedTeam, groupChar);
            //return true;
            //todo make method that returns si appartient a la zone 
            if (!IsGroupComplete())
            {
                if (IsValidForInsertionPotWise(droppedTeam))
                {
                    if (IsValidForInsertionContinentWise(droppedTeam))
                    {
                        AddTeam(droppedTeam);
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
            //todo method if appartient is it valid selon pot and group

            //todo create method that takes continent as arg and set bools to true value
            //set bools of continents if inserted 
            //++meter if complete insertion OK

            //check if complete and set isComplete to true (meter++)

        }
        
        private bool IsValidForInsertionPotWise(Team team)
        {
            return !ContainsPot[team.Pot - 1];
        }

        private bool IsValidForInsertionContinentWise(Team team)
        {
            
            if (_groupTeams.Count == 0)
                return true;

            if (team.IsAfrica)
                if (this.ContainsAfrica)
                    return false;

            if (team.IsAsia)
                if (this.ContainsAsia)
                    return false;

            if (team.IsEurope)
            {
                if (this.ContainsEuropeSecond)
                    return false;

                if (this.ContainsEuropeFirst)
                    return true;
            }
            
            if (team.IsSouthAmerica)
                if (this.ContainsSouthAmerica)
                    return false;

            if (team.IsNorthAmerica)
                if (this.ContainsNorthAmerica)
                    return false;

            return true;
        }

        


        private bool IsGroupComplete()
        {
            return (_groupTeams.Count == 4);
        }

        public Point PositionWhereToGo(Team team)
        {
            if (team.Pot < 0 || team.Pot > 4) 
                throw new Exception("Group::PositionWhereToGo() -> Unvalid position");

            return this.TEAMPOSITIONS[team.Pot - 1];
            
        }

        /// <summary>
        /// USE WITH CAUTION, ADD ONLY IF TEAM IS VALID FOR INSERTION OR YOU RISK LOOSING INTEGRITY OF GROUP
        /// </summary>
        private void AddTeam(Team teamToAdd)
        {
            //think of before adding
            //add exception if ggroup is full and insertions are trying to happen


            ContainsPot[teamToAdd.Pot - 1] = true;

            #region switch on teamToAdd.Continent 
            switch (teamToAdd.Continent)
            {
                case "africa":
                    this.ContainsAfrica = true;
                    break;
                case "asia":
                    this.ContainsAsia = true;
                    break;
                case "europe":
                    if (this.ContainsEuropeFirst) this.ContainsEuropeSecond = true;
                    else this.ContainsEuropeFirst = true;
                    break;
                case "northamerica":
                    this.ContainsNorthAmerica = true;
                    break;
                case "southamerica":
                    this.ContainsSouthAmerica = true;
                    break;
            }
            #endregion

            this._groupTeams.Add(teamToAdd);
            teamToAdd.RemoveEvents();
            teamToAdd.Group = _groupChar.ToString();

        }

        
    }
}