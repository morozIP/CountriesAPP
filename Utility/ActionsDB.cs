using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Windows;
using System.Xml.Serialization;

namespace CountriesAPP
{
    class ActionsDB
    {        
        public class CountryDBclass
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public string Capital { get; set; }
            public decimal Area { get; set; }
            public int Population { get; set; }
            public string Region { get; set; }
        }

        
        public ObservableCollection<Country> GetCountries()
        {
            DB dB = new DB();
            ObservableCollection<Country> countries = new ObservableCollection<Country>();
         
            string sqlExpression = @"SELECT Country.Name, Country.Code, City.Name, Country.Area, Country.Population, Region.Name
                                     FROM Country
                                     Join City ON Country.Capital = City.Id
                                     Join Region ON Country.Region = Region.Id";
            using (SqlConnection connection = dB.GetConnection())
            {
                dB.OpenConnection();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Country country = new Country((string)reader.GetValue(0), (string)reader.GetValue(1), (string)reader.GetValue(1), (string)reader.GetValue(2), (string)reader.GetValue(5), (int)reader.GetValue(4), (decimal)reader.GetValue(3));
                        countries.Add(country);
                    }

                }

                reader.Close();

                return countries;
            }

        }
        
        public void AddCountry(Country country)
        {
            DB dB = new DB();
            if (!CheckCountry(country))
            {
                object capitalID;
                object regionID;

                if (country.Capital.Length > 0)
                {
                    capitalID = CheckCapital(country.Capital);
                }
                else
                {
                    capitalID = DBNull.Value;
                }

                if (country.Region.Length>0)
                {
                    regionID = CheckRegion(country.Region);
                }
                else
                {
                    regionID = DBNull.Value;
                }

                string sqlExpression = "INSERT INTO Country (Name, Code, Capital, Area, Population, Region) VALUES (@name, @code, @capital, @area, @population, @region)";

                using (SqlConnection connection = dB.GetConnection())
                {
                    dB.OpenConnection();

                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.Parameters.AddWithValue("@name", country.Name);
                    command.Parameters.AddWithValue("@code", country.Alpha2Code);
                    command.Parameters.AddWithValue("@capital", capitalID);
                    command.Parameters.AddWithValue("@area", country.Area);
                    command.Parameters.AddWithValue("@population", country.Population);
                    command.Parameters.AddWithValue("@region", regionID);
                    object q = command.ExecuteNonQuery();
                    MessageBox.Show("Страна успешно добавлена");
                }

            }           


        }
        
        public void UpdateCountry(Country country, int id)
        {
            DB dB = new DB();
                object capitalID;
                object regionID;

                if (country.Capital.Length > 0)
                {
                    capitalID = CheckCapital(country.Capital);
                }
                else
                {
                    capitalID = DBNull.Value;
                }

                if (country.Region.Length > 0)
                {
                    regionID = CheckRegion(country.Region);
                }
                else
                {
                    regionID = DBNull.Value;
                }

                string sqlExpression = "UPDATE Country SET Code = @code , Capital= @capital, Area = @area, Population = @population, Region = @region WHERE Id = @id and Name = @name";

                using (SqlConnection connection = dB.GetConnection())
                {
                    dB.OpenConnection();

                    SqlCommand command = new SqlCommand(sqlExpression, connection);                    
                    command.Parameters.AddWithValue("@code", country.Alpha2Code);
                    command.Parameters.AddWithValue("@capital", capitalID);
                    command.Parameters.AddWithValue("@area", country.Area);
                    command.Parameters.AddWithValue("@population", country.Population);
                    command.Parameters.AddWithValue("@region", regionID);
                    command.Parameters.AddWithValue("@name", country.Name);
                    command.Parameters.AddWithValue("@id", id);
                    object q = command.ExecuteNonQuery();
                    MessageBox.Show("Страна успешно обновлена");
                }
        }

        #region Проверки

        protected bool CheckCountry(Country country)
        {
            DB dB = new DB();

            string sqlExpression = @"SELECT * FROM Country WHERE Name= @name";

            using (SqlConnection connection = dB.GetConnection())
            {
                dB.OpenConnection();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@name", country.Name);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UpdateCountry(country, (int)reader.GetValue(0));
                    }

                    reader.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    return false;
                }



            }


        }

        protected int CheckCapital(string capital)
        {
            DB dB = new DB();

            string sqlExpression = @"SELECT * FROM City WHERE Name= @capital";

            using (SqlConnection connection = dB.GetConnection())
            {
                dB.OpenConnection();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@capital", capital);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return (int)reader.GetValue(0);
                    }

                    reader.Close();


                }
                return AddCapital(capital);

            }


        }

        protected int CheckRegion(string region)
        {
            DB dB = new DB();

            string sqlExpression = @"SELECT * FROM Region WHERE Name= @region";

            using (SqlConnection connection = dB.GetConnection())
            {
                dB.OpenConnection();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@region", region);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return (int)reader.GetValue(0);
                    }

                    reader.Close();

                }
                return AddRegion(region);


            }


        }

        #endregion

       
        protected int AddCapital(string capital)
        {
            DB dB = new DB();

            string sqlExpression = "INSERT INTO City (Name) VALUES (@capital) SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = dB.GetConnection())
            {    

                dB.OpenConnection();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@capital", capital);

                
                return (int)(decimal)command.ExecuteScalar();
            }


        }
       
        protected int  AddRegion(string region)
        {
            DB dB = new DB();

            string sqlExpression = "INSERT INTO Region (Name) VALUES (@region) SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = dB.GetConnection())
            {
                dB.OpenConnection();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@region", region);
                
                return (int)(decimal)command.ExecuteScalar();
            }

        }
    }
}
