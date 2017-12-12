using System;
using System.Collections.Generic;
using System.Drawing;

namespace RepoRepo
{
    public class Group
    {
        private List<Team> _group = new List<Team>();

        //todo group should it know about pots logic ? or just stuff inside itself ? 
        //todo is it the engine property to manage right team from right pot to the right group ? so should it have the logic ?

        public readonly Point BORDERPOINT1;
        public readonly Point BORDERPOINT2;
        public readonly Point BORDERPOINT3;
        public readonly Point BORDERPOINT4;

        public readonly Point TEAM1POSITION;
        public readonly Point TEAM2POSITION;
        public readonly Point TEAM3POSITION;
        public readonly Point TEAM4POSITION;
        //...4

        private int _meter = 0; //could be subs with count-1

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
        
        public Group(Point border1, Point border2, Point border3, Point border4)
        {
            //init meter
            //init POSTEAMS
            TEAM1POSITION = border1;
            IsAfrica = IsEuropeFirst = IsAsia = IsEuropeSecond = IsNorthAmerica = IsSouthAmerica = false;
            _group.Add(null);
            BORDERPOINT1 = border1;
            BORDERPOINT2 = border2;
            BORDERPOINT3 = border3;
            BORDERPOINT4 = border4;
        }

        public bool AddTeamToGroup(Team teamToAdd)
        {
            //logic to add or not the team
            if (teamToAdd.IsAfrica)
            {
                if (IsAfrica)
                {
                    return false;
                }
                _group.Add(teamToAdd);
            }
            else if (teamToAdd.IsAsia)
            {
                if (IsAsia)
                {
                    return false;
                }
                _group.Add(teamToAdd);
            }
            else if (teamToAdd.IsEurope)
            {
                _group.Add(teamToAdd);
            }
            else if (teamToAdd.IsNorthAmerica)
            {
                if (IsNorthAmerica)
                {
                    return false;
                }
                _group.Add(teamToAdd);
            }
            else if (teamToAdd.IsSouthAmerica)
            {
                if (IsSouthAmerica)
                {
                    return false;
                }
                _group.Add(teamToAdd);
            }
            return false;
        }



        //todo put GetPosition as a static func in ENGINE

        public bool ProcesssDroppedTeam(Team droppedTeam)
        {
            //todo method if you could SET BOOLS IN CTOR TO FALSE OR RIGHT THING FIX YOUR STUFF

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
                    }
                else return false;
            }
            

            throw new Exception("ProcessDroppedTeam() -> Unhandled exception (neither false or true)");
            //todo method if appartient is it valid selon pot and group

            //todo create method that takes continent as arg and set bools to true value
            //set bools of continents if inserted 
            //++meter if complete insertion OK

            //check if complete and set isComplete to true (meter++)

        }
        
        private bool IsValidForInsertionPotWise(Team team)
        {
            //add logic here and return false if not
            return true;
        }

        private bool IsValidForInsertionContinentWise(Team team)
        {
            //logic if valid for insertion
            if (_group.Count == 1)
            {
                return true;
            }
            if (team.IsAfrica)
            {
                if (this.IsAfrica)
                {
                    return false;
                }
            }else if (team.IsAsia)
            {
                if (this.IsAsia)
                {
                    return false;
                }
            }else if (team.IsEurope)
            {
                if (this.IsEuropeFirst)
                {
                    IsEuropeSecond = true;                  //todo <============== be careful with this statement
                    return true;
                }
                if (this.IsEuropeSecond)
                {
                    return false;
                }
            }else if (team.IsSouthAmerica)
            {
                if (this.IsSouthAmerica)
                {
                    return false;
                }
            } else if (IsNorthAmerica)
            {
                if (this.IsNorthAmerica)
                {
                    return false;
                }
            }
            //handle when group is full
            throw new Exception("IsValidInsertion() -> Unhandled case of a team.");
        }

        

        private bool IsGroupComplete()
        {
            //check count of list == 5 ?
            return false;
            
        }

        public Point PositionWhereToGo(Team team)
        {
            switch (team.Pot) //sub with count of list
            {
                case 1: return this.TEAM1POSITION;
                case 2: return this.TEAM1POSITION;
                case 3: return this.TEAM3POSITION;
                case 4: return this.TEAM4POSITION;
            }
            throw new Exception("Team::PositionWhereToGo() -> Unvalid _meter");
        }

        /// <summary>
        /// USE WITH CAUTION, ADD ONLY IF TEAM IS VALID FOR INSERTION OR YOU RISK LOOSING INTEGRITY OF GROUP
        /// </summary>
        private void AddTeam(Team teamToAdd)
        {
            //think of before adding
            //add exception if ggroup is full and insertions are trying to happen
            this._group.Add(teamToAdd);
            _meter++;
            //set state of group after adding team
        }
        
    }
}