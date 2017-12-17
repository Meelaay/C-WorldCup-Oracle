﻿using System;
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
        public string ConnectionString { get; }
        public static OracleConnection _myConnection = new OracleConnection();



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

        public void AddScheduleToDataBase(List<Match> matchesList)
        {
            foreach (var match in matchesList)
                AddMatchToDataBase(match);
        }

        public void AddMatchToDataBase(Match matchToAdd)
        {
            string query = string.Format(
                    "INSERT INTO matches VALUES(matches_seq.nextval, '{0}', '{1}', 0, 0, TO_DATE('{2}/{3}/{4}','dd-mm-yyyy'))",
                    matchToAdd.Team1.Name,
                    matchToAdd.Team2.Name, 
                    matchToAdd.MatchDate.Day, matchToAdd.MatchDate.Month, matchToAdd.MatchDate.Year
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

        public void EstablishConnection()
        {
            var a = ExecuteQuery("select * from test");

        }

        public static void UpdateGroupForEachTeamInDataBase(List<Group> groupsList)
        {
            foreach (var group in groupsList)
                foreach (var team in group.GetGroupTeams())
                    UpdateTeamInDataBase(team);
        }

        public static void UpdateTeamInDataBase(Team newTeam)
        {
            string query = string.Format("UPDATE teams SET groupT = '{0}' WHERE country = '{1}'",
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
            string query = String.Format("SELECT country, pot, continent FROM teams t WHERE t.pot = '{0}' ", pot);//pot is 1-2-3-4 not a-b-c-d
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

    }
}