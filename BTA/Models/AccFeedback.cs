//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BTA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class AccFeedback
    {
        public int AccFeedbackID { get; set; }
        public int UserID { get; set; }
       
        public int StarRating { get; set; }
        public string Comment { get; set; }
        public System.DateTime DateTimeOfFeedback { get; set; }
        public int AccommodationID { get; set; }
    
        public virtual Accommodation Accommodation { get; set; }
        public virtual User User { get; set; }
    }
}
