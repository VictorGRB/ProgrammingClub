using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProgrammingClubM.Models;
using ProgrammingClubM.Models.DBObjects;

namespace ProgrammingClubM.Repository
{
    public class CodeSnippetRepository
    {
        private ClubMembershipModelsDataContext clubmembershipDataContext;

        public CodeSnippetRepository()
        {
            this.clubmembershipDataContext = new ClubMembershipModelsDataContext();
        }

        public List<CodeSnippetModel> GetAllCodeSnippets()
        {
            List<CodeSnippetModel> codeSnippetModels = new List<CodeSnippetModel>();

            foreach (CodeSnippet codeSnippet in clubmembershipDataContext.CodeSnippets)
            {
                codeSnippetModels.Add(MapDbObjectToModel(codeSnippet));
            }
            return codeSnippetModels;
        }
        public CodeSnippetModel GetCodeSnippetID(Guid id)
        {
            CodeSnippetModel codeSnippetModel = new CodeSnippetModel();

            CodeSnippet codeSnippet = clubmembershipDataContext.CodeSnippets.FirstOrDefault(x => x.IDCodeSnippet == id);

            return MapDbObjectToModel(codeSnippet);
        }

        public List<CodeSnippetModel> GetCodeSnippetsByTimeAdded(DateTime eventDate)
        {
            List<CodeSnippetModel> codeSnippetModels = new List<CodeSnippetModel>();

            foreach (CodeSnippet codeSnippet in clubmembershipDataContext.CodeSnippets.Where
                (x => x.DateTimeAdded.Date == eventDate.Date))
            {
                codeSnippetModels.Add(MapDbObjectToModel(codeSnippet));
            }
            return codeSnippetModels;
        }

        public void InsertCodeSnippet(CodeSnippetModel codeSnippetModel)
        {
            codeSnippetModel.IDCodeSnippet = Guid.NewGuid();

            clubmembershipDataContext.CodeSnippets.InsertOnSubmit(MapModelToDbObject(codeSnippetModel));
            clubmembershipDataContext.SubmitChanges();
        }

        public void UpdateCodeSnippet(CodeSnippetModel codeSnippetModel)
        {
            Models.DBObjects.CodeSnippet existingCodeSnippet = clubmembershipDataContext.CodeSnippets.FirstOrDefault
                (x => x.IDCodeSnippet == codeSnippetModel.IDCodeSnippet);
            if (existingCodeSnippet != null)
            {
                existingCodeSnippet.IDCodeSnippet = codeSnippetModel.IDCodeSnippet;
                existingCodeSnippet.ContentCode = codeSnippetModel.ContentCode;
                existingCodeSnippet.IDMember = codeSnippetModel.IDMember;
                existingCodeSnippet.Title = codeSnippetModel.Title;
                existingCodeSnippet.Revision = codeSnippetModel.Revision;
                existingCodeSnippet.DateTimeAdded = codeSnippetModel.DateTimeAdded;
                existingCodeSnippet.IDSnippetPreviousVersion = codeSnippetModel.IDSnippetPreviousVersion;
                existingCodeSnippet.IsPublished = codeSnippetModel.IsPublished;
                clubmembershipDataContext.SubmitChanges();
            }
        }

        public void DeleteCodeSnippet(Guid ID)
        {
            CodeSnippet codeSnippetToDelete = clubmembershipDataContext.CodeSnippets.FirstOrDefault
                (x => x.IDCodeSnippet == ID);
            if (codeSnippetToDelete != null)
            {
                clubmembershipDataContext.CodeSnippets.DeleteOnSubmit(codeSnippetToDelete);
                clubmembershipDataContext.SubmitChanges();
            }
        }

        private CodeSnippetModel MapDbObjectToModel(CodeSnippet codeSnippet)
        {
            CodeSnippetModel codeSnippetModel = new CodeSnippetModel();
            if (codeSnippet != null)
            {
                codeSnippetModel.IDCodeSnippet = codeSnippet.IDCodeSnippet;
                codeSnippetModel.DateTimeAdded = codeSnippet.DateTimeAdded;
                codeSnippetModel.IDSnippetPreviousVersion = codeSnippet.IDSnippetPreviousVersion;
                codeSnippetModel.Revision = codeSnippet.Revision;
                codeSnippetModel.Title = codeSnippet.Title;
                codeSnippetModel.ContentCode = codeSnippet.ContentCode;
                codeSnippetModel.IDMember = codeSnippet.IDMember;
                codeSnippetModel.IsPublished = codeSnippet.IsPublished;

                return codeSnippetModel;
            }
            return null;
        }
        private CodeSnippet MapModelToDbObject(CodeSnippetModel codeSnippetModel)
        {
            CodeSnippet dbCodeSnippetModel = new CodeSnippet();
            if (codeSnippetModel != null)
            {
                dbCodeSnippetModel.IDCodeSnippet = codeSnippetModel.IDCodeSnippet;
                dbCodeSnippetModel.ContentCode = codeSnippetModel.ContentCode;
                dbCodeSnippetModel.IDMember = codeSnippetModel.IDMember;
                dbCodeSnippetModel.Title = codeSnippetModel.Title;
                dbCodeSnippetModel.Revision = codeSnippetModel.Revision;
                dbCodeSnippetModel.DateTimeAdded = codeSnippetModel.DateTimeAdded;
                dbCodeSnippetModel.IDSnippetPreviousVersion = codeSnippetModel.IDSnippetPreviousVersion;
                dbCodeSnippetModel.IsPublished = codeSnippetModel.IsPublished;
                return dbCodeSnippetModel;
            }
            return null;



        }




    }
}
