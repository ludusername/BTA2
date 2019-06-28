using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BTA.Models
{
    public class TransportationProviderViewModel
    {
        public int TransportationProviderID { get; set; }
        [Required(ErrorMessage = "You must enter name of transportation provider")]
        public string TransportationProviderName { get; set; }
        public int CountryID { get; set; }
        [Required(ErrorMessage = "You must enter street name of transportation provider")]
        public string StreetName { get; set; }
        [Required(ErrorMessage = "You must enter street number of transportation provider")]
        public string StreetNumber { get; set; }
        [Required(ErrorMessage = "Phone Number is needed.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$", ErrorMessage = "The PhoneNumber field is not a valid phone number")]
        

        public string Phone { get; set; }
        public int TransportaitonTypeID { get; set; }
        public string TransportaitonTypeName { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        [NotMapped]
        public List<TransportaitonProvider> provajderi { get; set; }
        [NotMapped]
        public List<TransportaitonProvider> sifaragradova { get; set; }
        [NotMapped]
        public List<City>  gradovi { get; set; }
        [NotMapped]
        public List<Country> drzave { get; set; }
        [NotMapped]
        public List<TransportaitonType> tipovitransporta { get; set; }
    }
}