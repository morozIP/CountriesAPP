using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;

namespace CountriesAPP
{
    public class Country
    {        
        public string Name { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public string Capital { get; set; }
        public string Region { get; set; }
        public int Population { get; set; }
        public decimal Area { get; set; }


        public Country(string _name, string _alpha2, string _alpha3, string _capital, string _region, int _population, decimal _area)
        {
            Name = _name;
            Alpha2Code = _alpha2;
            Alpha3Code = _alpha3;
            Capital = _capital;
            Region = _region;
            Population = _population;
            Area = _area;
        }

    }

}

