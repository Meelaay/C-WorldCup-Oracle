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
        public string ConnectionString { get; }
        public OracleConnection _myConnection = new OracleConnection();



        public DataBaseConnection()
        {
            // for Amine use : 192.168.234.130
            // for Reda use  : 10.77.0.2
            const string host = "192.168.234.130";
            const string port = "1521";
            const string sid = "XE";
            const string user = "worldcup";
            const string pass = "password";

            ConnectionString = String.Format(
                "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1}))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = XE)));User Id={2};password={3};",
                host, port, user, pass);

            _myConnection.ConnectionString = ConnectionString;
        }

        public static void AddMatchToDataBase()
        {
            
        }

        public void EstablishConnection()
        {
            var a = ExecuteQuery("select * from test");

        }

        public void MigrateToDataBase(Group groupA, Group groupB, Group groupC, Group groupD,
                                      Group groupE, Group groupF, Group groupG, Group groupH )
        {
            //transfer all foreach redundant logic from engine here and call this once
            Team team = new Team(null, null,null, null);
            UpdateTeamInDataBase(team);
        }

        public void UpdateTeamInDataBase(Team newTeam)
        {
            string query = string.Format("UPDATE teams SET groupT = '{0}' WHERE country = '{1}'",
                                          newTeam.Group, newTeam.Name);
            _myConnection.Open();
            OracleCommand cmd = new OracleCommand(query);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = _myConnection;
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected != 1)
            {
                throw new Exception("DataBaseConnection::UpdateTeamInDataBase() -> rowsAffected != 1");
            }
            _myConnection.Close();
        }


        internal DataTable ExecuteQuery(string query)
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

        /* initial ExecuteQuery() with test
        private DataTable ExecuteQuery(string query)
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
            string test = "";

            foreach ( DataRow row in dt.Rows)
            {
                test = row["NAME"].ToString();
            }
            return dt;
        }
        */





    }
}