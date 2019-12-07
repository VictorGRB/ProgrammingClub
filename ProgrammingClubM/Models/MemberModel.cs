using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProgrammingClubM.Models
{
    public class MemberModel
    {
        
        public Guid IDMember { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(20, ErrorMessage = "Name too long")]
        [MinLength(2, ErrorMessage = "You have failed this city !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(30, ErrorMessage = "Title too long")]
        [MinLength(2, ErrorMessage = "You have failed this city !")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(20, ErrorMessage = "Position too long")]
        [MinLength(2, ErrorMessage = "You have failed this city !")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(1000, ErrorMessage = "Description too long")]
        [MinLength(2, ErrorMessage = "You have failed this city !")]

        public string Description { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(1000, ErrorMessage = "Description too long")]
        [MinLength(2, ErrorMessage = "You have failed this city !")]
        public string Resume { get; set; }
    }
}