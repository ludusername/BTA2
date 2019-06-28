using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BTA.Models
{
    public class CityModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int CountryID { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string CountryName { get; set; }

        [NotMapped]
        List<City> allsearch { get; set; }
    }
}