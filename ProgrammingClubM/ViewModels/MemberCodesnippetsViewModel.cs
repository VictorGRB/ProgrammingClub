
using ProgrammingClubM.Models;
using ProgrammingClubM.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammingClubM.ViewModels
{
    public class MemberCodesnippetsViewModel
    {
        public string Name { get; set; }

        public string Title { get; set; }
        public string Position { get; set; }
        public List<CodeSnippetModel> codeSnippets = new List<CodeSnippetModel>();
    }
}