using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        
        public void EstablishConnection()
        {
            var a = ExecuteQuery("select * from test");

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