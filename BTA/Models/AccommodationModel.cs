using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BTA.Models
{
    public class AccommodationModel
    {
        public int AccommodationID { get; set; }
        [Required(ErrorMessage ="Please enter accomodation")]
        public string Name { get; set; }
        public int AccCategoryID { get; set; }
        public int CityID { get; set; }
        [Required(ErrorMessage ="Please enter adress of accomodation")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please rate this accomodation")]
        [Range(1, 5, ErrorMessage = ("Rate this accomodation from rate:1-5"))]
        public Nullable<int> StarRating1 { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone Number is needed.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$", ErrorMessage = "The PhoneNumber field is not a valid phone number")]
        public string Phone { get; set; }
        [Required(ErrorMessage ="Please enter description of accomodation")]
        public string Description { get; set; }
        public string Name1 { get; set; }
        public string CityName { get; set; }
        public string Comment { get; set; }
        public System.DateTime DateTimeOfFeedback { get; set; }
        public int UserID { get; set; }
        public int AccFeedbackID { get; set; }
        public string UserName { get; set; }
        public Nullable<double> prosek { get; set; }
        [NotMapped]
        public List<TransportaitonProvider> provajderi { get; set; }
        [NotMapped]
        public List<TransportaitonProvider> sifaragradova { get; set; }
        [NotMapped]
        public List<City> gradovi { get; set; }
        [NotMapped]
        public List<Country> drzave { get; set; }
        [NotMapped]
        public List<TransportaitonType> tipovitransporta { get; set; }
        [NotMapped]
        public List<AccCategory> tipsmestaja { get; set; }
    }
}