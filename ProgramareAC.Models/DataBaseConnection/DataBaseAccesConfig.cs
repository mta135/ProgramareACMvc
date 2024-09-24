using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProgramareAC.DataBaseConnection
{
    public class DataBaseAccesConfig
    {
        private SqlConnection connection;

        public SqlConnection Connection
        {

            get
            {

                SetConnection();
                return this.connection;
            }
        }


        private void SetConnection()
        {
            string connectionString = GetConnectionString();
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["eCerereConString"].ConnectionString;
        }

        public void Dispose()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

    }
}