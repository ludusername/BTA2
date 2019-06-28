using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace BTA.Models
{
    public class TransportaitonModel
    {
        public int TransportaitonID { get; set; }
        public int CityFromID { get; set; }
        public int CityToID { get; set; }
        public int TransportaitonProviderID { get; set; }
        public int TransportaionTypeID { get; set; }
        [Required(ErrorMessage = "Please enter city you want to find")]
        public string CityName { get; set; }
        [Required(ErrorMessage = "Please enter city you want to find")]
        public string CityName1 { get; set; }
        public string TransportationProviderName { get; set; }
        public string TransportaitonTypeName { get; set; }
        [Required(ErrorMessage ="Please enter your comment")]
        public string Comment { get; set; }
        [Required(ErrorMessage = "Please rate this accomodation")]
        [Range(1, 5, ErrorMessage = ("Rate this accomodation from rate:1-5"))]
        public int StarRating { get; set; }
        public string UserName { get; set; }
        public Nullable<double> prosek { get; set; }
        public int UserID { get; set; }
        public int CityID { get; set; }
        public int RatingID { get; set; }
        public System.DateTime Date { get; set; }
        public System.DateTime Date1 { get; set; }
        public int FeedBackID { get; set; }
        public int CommentID { get; set; }


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
    }
}