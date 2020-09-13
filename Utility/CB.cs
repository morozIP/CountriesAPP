using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace CountriesAPP.Utility
{
    class CB
    {
        string connectionString;
        SqlConnection connection; 
        public CB(string[] values)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = values[0];
            builder.InitialCatalog = values[1];
            builder.UserID = values[2];
            builder.Password = values[3];
            connectionString = builder.ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}
