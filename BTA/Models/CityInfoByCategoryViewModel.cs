using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BTA.Models
{
    public class CityInfoByCategoryViewModel
    {
        public int CityID { get; set; }
        [Required(ErrorMessage ="Please enter city you want to find")]
        public string CityName { get; set; }
        public string CityCategoryName { get; set; }
        public string UserName { get; set; }
        public int CityCategoryID { get; set; }
        public string Info { get; set; }
        public int UserID { get; set; }
        public int CommentID { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime Date { get; set; }
        [Required(ErrorMessage ="Please enter your comment")]
        public string Comment { get; set; }
        public int CategoryID { get; set; }

    }
}