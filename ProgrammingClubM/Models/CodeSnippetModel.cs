using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ProgrammingClubM.Models
{
    public class CodeSnippetModel
    {
        public Guid IDCodeSnippet { get; set; }
        [Required(ErrorMessage="Mandatory Field")]
        [StringLength(50,ErrorMessage ="Too long")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        [StringLength(150, ErrorMessage = "Too long")]
        public string ContentCode { get; set; }
        public Guid IDMember { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        
        public int Revision { get; set; }
        public Guid? IDSnippetPreviousVersion { get; set; }
        public DateTime DateTimeAdded { get; set; }
        
        public bool IsPublished { get; set; }
}
}