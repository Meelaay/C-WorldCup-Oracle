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

        public DataBaseConnection()
        {
            // for Amine use : 192.168.234.130
            // for Reda use  : 10.77.0.2
            string host = "192.168.234.130";
            string port = "1521";
            string sid = "xe";
            string user = "system";
            string pass = "password";
            string f =
                $"Data Source = (DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port}))(CONNECT_DATA=(SID={sid})));uid={user};pwd={pass};";

            ConnectionString = $"Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = {host})(PORT = {port}))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = XE)));User Id={user};password={pass};";
            _myConnection.ConnectionString = ConnectionString;
        }

        public OracleConnection _myConnection = new OracleConnection();
        
        public void EstablishConnection()
        {
            var a = ExecuteQuery("select * from test");

        }

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
                //KHDMAAAAT reda bdel ip lfou9w jreb, matnsach tzid breakpoint 7da test w tinspecti khassk tl9aha 5
            }
            return dt;
        }




    }
}