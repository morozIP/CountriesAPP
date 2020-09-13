using System;
using System.Data.SqlClient;
using System.Windows;

namespace CountriesAPP
{
    class DB
    {
        SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=CountryDB;Integrated Security=True");

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

