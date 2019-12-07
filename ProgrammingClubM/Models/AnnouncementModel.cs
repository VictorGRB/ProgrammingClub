using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgrammingClubM.Models
{
    public class AnnouncementModel
    {

        [Required(ErrorMessage="Mandatory Field")]
        public Guid IDAnnouncement { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        public DateTime ValidFrom { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        public DateTime ValidTo { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        [StringLength(20, ErrorMessage="Title too long(max 20 chars)")]

        public string Title { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        [StringLength(50, ErrorMessage = "Text too long(max 50 chars)")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]

        public DateTime? EventDateTime { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        [StringLength(20, ErrorMessage = "Tags too long(max20 chars)")]
        public string Tags { get; set; }

        //public int ValidityDays { get; set; }
    }
}