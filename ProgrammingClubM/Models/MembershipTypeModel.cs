using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ProgrammingClubM.Models
{
    public class MembershipTypeModel
    {
        public Guid IDMembershipType { get; set; }
        [Required(ErrorMessage="Mandatory Field")]
        [MaxLength(50,ErrorMessage ="Too many characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        [MaxLength(150, ErrorMessage = "Too many characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        public int SubscriptionLengthInMonths { get; set; }
    }
}