using System;
using System.Collections.Generic;
using System.Drawing;

namespace RepoRepo
{
    public class Group
    {
        private List<Team> _group = new List<Team>();

        private Schedule _schedule = new Schedule();

        //todo group should it know about pots logic ? or just stuff inside itself ? 
        //todo is it the engine property to manage right team from right pot to the right group ? so should it have the logic ?

        #region Group : Borders and positions
        public Point BORDERPOINT1;
        public Point BORDERPOINT2;
        public Point BORDERPOINT3;
        public Point BORDERPOINT4;

        public Point TEAM1POSITION;
        public Point TEAM2POSITION;
        public Point TEAM3POSITION;
        public Point TEAM4POSITION;
        #endregion

        public Schedule GetSchedule()
        {
            return _schedule;
        }

        public List<Team> GetGroupTeams() //=> _group
        {
            return _group;
        }

        public bool IsComplete { get; set; } //could be substituted with count of list == 5

        public bool IsAfrica { get; set; }
        public bool IsAsia { get; set; }
        public bool IsEuropeFirst { get; set; }
        public bool IsEuropeSecond { get; set; }
        public bool IsNorthAmerica { get; set; }
        public bool IsSouthAmerica { get; set; }

        public bool IsPot1 { get; set; }
        public bool IsPot2 { get; set; }
        public bool IsPot3 { get; set; }
        public bool IsPot4 { get; set; }
        
        public Group(Point p1, Point p3, Point firstElement)
        {

            FillBordersOfGroup(p1, p3);
            FillTeamPositionsInGroup(firstElement);

            IsAfrica = IsEuropeFirst = IsAsia = IsEuropeSecond = IsNorthAmerica = IsSouthAmerica
                     = IsPot1 = IsPot2 = IsPot3 = IsPot4 = false;

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

            BORDERPOINT1 = p1;
            BORDERPOINT2 = p2;
            BORDERPOINT3 = p3;
            BORDERPOINT4 = p4;
        }
        private void FillTeamPositionsInGroup(Point firstElement)
        {
            //pass the first one as arg and then += y
            int x = firstElement.X; int y = firstElement.Y;

            TEAM1POSITION = new Point(x, y);
            y += 42;
            TEAM2POSITION = new Point(x, y);
            y += 41;
            TEAM3POSITION = new Point(x, y);
            y += 42;
            TEAM4POSITION = new Point(x, y);
        }


        

        //todo put GetPosition as a static func in ENGINE

        public bool ProcesssDroppedTeam(Team droppedTeam, char groupChar)
        {
            //todo method if you could SET BOOLS IN CTOR TO FALSE OR RIGHT THING FIX YOUR STUFF
            
            //todo make method that returns si appartient a la zone 
            if (!IsGroupComplete())
            {
                if (IsValidForInsertionPotWise(droppedTeam))
                {
                    if (IsValidForInsertionContinentWise(droppedTeam))
                    {
                        AddTeam(droppedTeam, groupChar);
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
            switch (team.Pot)
            {
                case 1:
                    if (IsPot1) return false;
                    break;
                case 2:
                    if (IsPot2) return false;
                    break;
                case 3:
                    if (IsPot3) return false;
                    break;
                case 4:
                    if (IsPot4) return false;
                    break;
            }
            
            return true;
        }

        private bool IsValidForInsertionContinentWise(Team team)
        {
            
            if (_group.Count == 0)
                return true;

            if (team.IsAfrica)
                if (this.IsAfrica)
                    return false;

            if (team.IsAsia)
                if (this.IsAsia)
                    return false;

            if (team.IsEurope)
            {
                if (this.IsEuropeSecond)
                    return false;

                if (this.IsEuropeFirst)
                    return true;
            }
            
            if (team.IsSouthAmerica)
                if (this.IsSouthAmerica)
                    return false;

            if (team.IsNorthAmerica)
                if (this.IsNorthAmerica)
                    return false;

            return true;
        }

        


        private bool IsGroupComplete()
        {
            return (_group.Count == 4);
        }

        public Point PositionWhereToGo(Team team)
        {
            switch (team.Pot) //sub with count of list
            {
                case 1: return this.TEAM1POSITION;
                case 2: return this.TEAM2POSITION;
                case 3: return this.TEAM3POSITION;
                case 4: return this.TEAM4POSITION;
            }
            throw new Exception("Group::PositionWhereToGo() -> Unvalid _meter");
        }

        /// <summary>
        /// USE WITH CAUTION, ADD ONLY IF TEAM IS VALID FOR INSERTION OR YOU RISK LOOSING INTEGRITY OF GROUP
        /// </summary>
        private void AddTeam(Team teamToAdd, char groupChar)
        {
            //think of before adding
            //add exception if ggroup is full and insertions are trying to happen

            #region switch on teamToAdd.Pot
            switch (teamToAdd.Pot)
            {
                case 1:
                    this.IsPot1 = true;
                    break;
                case 2:
                    this.IsPot2 = true;
                    break;
                case 3:
                    this.IsPot3 = true;
                    break;
                case 4:
                    this.IsPot4 = true;
                    break;
            }
            #endregion

            #region switch on teamToAdd.Continent 
            switch (teamToAdd.Continent)
            {
                case "africa":
                    this.IsAfrica = true;
                    break;
                case "asia":
                    this.IsAsia = true;
                    break;
                case "europe":
                    if (this.IsEuropeFirst) this.IsEuropeSecond = true;
                    else this.IsEuropeFirst = true;
                    break;
                case "northamerica":
                    this.IsNorthAmerica = true;
                    break;
                case "southamerica":
                    this.IsSouthAmerica = true;
                    break;
            }
            #endregion

            this._group.Add(teamToAdd);
            teamToAdd.RemoveEvents();
            teamToAdd.Group = groupChar.ToString();

        }

        
    }
}