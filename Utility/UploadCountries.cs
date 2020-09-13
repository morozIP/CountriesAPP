using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;

namespace CountriesAPP
{
    class UploadCountries
    {
        string _response;
        
        ObservableCollection<Country> countries { get; set; }

        public UploadCountries()
        {
            try
            {
                WebRequest request = WebRequest.Create("https://restcountries.eu/rest/v2/all?fields=all;name;alpha2Code;alpha3Code;capital;area;population;region");
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        var line = "";
                        while ((line = reader.ReadLine()) != null)
                        {
                            _response = line;
                        }

                        reader.Close();
                    }
                    stream.Close();

                }
                response.Close();

                countries = JsonConvert.DeserializeObject<ObservableCollection<Country>>(_response);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public ObservableCollection<Country> GetCountries()
        {
            return countries;
        }
    }
}
