using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace RepoRepo
{
    public class DataBaseConnection
    {
        private string ConnectionString { get; }
        private static OracleConnection _myConnection = new OracleConnection();



        public DataBaseConnection()
        {
            // for Amine use : 192.168.234.130
            // for Reda use  : 10.77.0.2
            const string host = "192.168.234.130";
            const string port = "1521";
            const string sid = "XE";
            const string user = "worldcup";
            const string pass = "password";

            ConnectionString = string.Format(
                "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1}))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = {2})));User Id={3};password={4};",
                host, port, sid, user, pass);

            _myConnection.ConnectionString = ConnectionString;
        }

        public void UpdateTeamsStats(List<BasicTeam> teamsList)
        {
            foreach (var team in teamsList)
            {
                UpdateTeamStats(team);
            }
        }

        private void UpdateTeamStats(BasicTeam team)
        {
            //if the app shows a bug or a delay -> reset the sequence to 0, transcate matches table, AND COMMIT
            string query = string.Format("UPDATE team t SET t.points = {0}, t.win = {1}, t.draw = {2}, t.loss = {3} WHERE country = '{4}'",
                    team.points,
                    team.win,
                    team.draw,
                    team.loss,
                    team.Name
                );

            _myConnection.Open();
                OracleCommand cmd = new OracleCommand(query);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = _myConnection;
                int rowsAffected = cmd.ExecuteNonQuery();
            _myConnection.Close();

            if (rowsAffected != 1)
                throw new Exception("DataBaseConnection::UpdateTeamStats() -> rowsAffected != 1");
        }



        public void UpdateMatchesInDataBase(List<Match> matches)
        {
            foreach (var match in matches)
                UpdateMatchInDataBase(match);   
        }

        private void UpdateMatchInDataBase(Match match)
        {
            string query = string.Format("UPDATE match m SET m.score1 = {0}, m.score2 = {1} WHERE DEREF(m.team1).country = '{2}' AND DEREF(m.team2).country = '{3}'",
                    match.score1.Text,
                    match.score2.Text,
                    match.Team1.Name,
                    match.Team2.Name
                );

            _myConnection.Open();
                OracleCommand cmd = new OracleCommand(query);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = _myConnection;
                int rowsAffected = cmd.ExecuteNonQuery();
            _myConnection.Close();

            if (rowsAffected != 1)
                throw new Exception("DataBaseConnection::UpdateMatchInDataBase() -> rowsAffected != 1");
        }

        public void AddScheduleToDataBase(List<Match> matchesList)
        {
            foreach (var match in matchesList)
                AddMatchToDataBase(match);
        }

        public void AddMatchToDataBase(Match matchToAdd)
        {
            string query = string.Format(
                    "INSERT INTO match VALUES(t_match(match_seq.nextval, TO_DATE('{0}/{1}/{2}', 'dd-mm-yyyy'), 32, 0, 0, (SELECT REF(t) FROM team t WHERE t.country = '{3}'), (SELECT REF(t) FROM team t WHERE t.country = '{4}')))",
                    matchToAdd.MatchDate.Day, matchToAdd.MatchDate.Month, matchToAdd.MatchDate.Year,
                    matchToAdd.Team1.Name,
                    matchToAdd.Team2.Name 
                );

            _myConnection.Open();
            OracleCommand cmd = new OracleCommand(query);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _myConnection;
            int rowsAffected = cmd.ExecuteNonQuery();
            _myConnection.Close();

            if (rowsAffected != 1)
                throw new Exception("DataBaseConnection::AddMatchInDataBase() -> rowsAffected != 1");
        }


        public static void UpdateGroupForEachTeamInDataBase(List<Group> groupsList)
        {
            foreach (var group in groupsList)
                foreach (var team in group.GetGroupTeams())
                    UpdateTeamInDataBase(team);
        }

        public static void UpdateTeamInDataBase(Team newTeam)
        {
            string query = string.Format("UPDATE team SET groupT = '{0}' WHERE country = '{1}'",
                                          newTeam.Group, newTeam.Name);
            _myConnection.Open();
            OracleCommand cmd = new OracleCommand(query);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _myConnection;
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected != 1)
                throw new Exception("DataBaseConnection::UpdateTeamInDataBase() -> rowsAffected != 1");

            _myConnection.Close();
        }


        private static DataTable ExecuteQuery(string query)
        {
            _myConnection.Open();
            DataTable dt = new DataTable();
            OracleCommand cmd = new OracleCommand(query);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _myConnection;
            
            using (OracleDataAdapter dataAdapter = new OracleDataAdapter())
            {
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dt);
            }
            _myConnection.Close();
            
            return dt;
        }
        public static Team RowToTeam(DataRow row)
        {
            string name = row["COUNTRY"].ToString();
            string continent = row["CONTINENT"].ToString();
            string pot = row["POT"].ToString();

            string path = @"..\..\Sprites\teams\" + name + ".png";

            Team team = new Team(path, name, continent, pot);
            if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(name)) throw new NullReferenceException("RowToTeam() -> path or name is empty.");

            return team;
        }
        public static List<Team> FillPotFromDataBase(int pot, List<Point> initialPositionsList)
        {
            //todo not * fix IT ** you can change it here if it requirements chage in future
            var teamsListOfPot = new List<Team>();
            string query = String.Format("SELECT country, pot, continent FROM team t WHERE t.pot = '{0}' ", pot);//pot is 1-2-3-4 not a-b-c-d
            DataTable dt = ExecuteQuery(query);

            int i = -1;

            if      (pot == 1) i = 0;
            else if (pot == 2) i = 8;
            else if (pot == 3) i = 16;
            else if (pot == 4) i = 24;

            if (i != 0 && i != 8 && i != 16 && i != 24)
                throw new Exception("DataBaseConnection::FillPotFromDataBase() -> Unvalid pot.");

            foreach (DataRow row in dt.Rows)
            {
                var team = RowToTeam(row);
                team.SetFlagPosition(initialPositionsList[i++]);
                teamsListOfPot.Add(team);
            }

            if (teamsListOfPot.Count == 0)
                throw new NullReferenceException("DataBaseConnection::FillPotFromDataBase() -> teamsListOfPot() is empty.");

            return teamsListOfPot;
        }

        public List<List<string>> ChampionsOfGroup(List<Group> groupsList)
        {
            List<List<string>> chamList = new List<List<string>>(capacity:2);
            List<string> firstplace = new List<string>(capacity:8);
            List<string> secondplace = new List<string>(capacity: 8);


            foreach (var group in groupsList)
            {
                var a = FirstTwoTeams(group._groupChar);
                firstplace.Add(a[0]);
                secondplace.Add(a[1]);
            }

            chamList.Add(firstplace);
            chamList.Add(secondplace);


            //code to fill list from db

            return chamList;
        }

        private List<string> FirstTwoTeams(char groupChar)
        {
            string query = string.Format(
                "SELECT * FROM (SELECT * FROM team t WHERE t.groupT = '{0}' ORDER BY t.points DESC) WHERE ROWNUM <= 2",
                groupChar.ToString()
            );

            List<string> firstAndSecond = new List<string>(capacity:2);

            var dt = ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                string name = row["COUNTRY"].ToString();
                firstAndSecond.Add(name);
            }

            return firstAndSecond;
        }
    }
}