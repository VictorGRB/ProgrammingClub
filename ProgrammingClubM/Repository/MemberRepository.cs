using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProgrammingClubM.Models;
using ProgrammingClubM.Models.DBObjects;
using ProgrammingClubM.ViewModels;

namespace ProgrammingClubM.Repository
{
    public class MemberRepository
    {
        private ClubMembershipModelsDataContext clubmembershipDataContext;

        public MemberRepository()
        {
            this.clubmembershipDataContext = new ClubMembershipModelsDataContext();
        }
        public MemberCodesnippetsViewModel GetMemberCodeSnippets(Guid memberID)
        {
            MemberCodesnippetsViewModel memberCodesnippetsViewModel = new MemberCodesnippetsViewModel();

            Member member = clubmembershipDataContext.Members.FirstOrDefault(x => x.IDMember == memberID);

            if (member != null)
            {
                memberCodesnippetsViewModel.Name = member.Name;
                memberCodesnippetsViewModel.Position = member.Name;
                memberCodesnippetsViewModel.Title = member.Title;

               IQueryable<CodeSnippet> memberCodeSnippets=clubmembershipDataContext.CodeSnippets.Where(x => x.IDMember == memberID);

                foreach(CodeSnippet dbCodeSnippet in memberCodeSnippets)
                {
                    CodeSnippetModel codeSnippetModel = new CodeSnippetModel();

                    codeSnippetModel.Title = dbCodeSnippet.Title;
                    codeSnippetModel.ContentCode = dbCodeSnippet.ContentCode;
                    codeSnippetModel.Revision = dbCodeSnippet.Revision;
                    memberCodesnippetsViewModel.codeSnippets.Add(codeSnippetModel);
                }
            }
            return memberCodesnippetsViewModel;
        }
        public List<MemberModel> GetAllMembers()
        {
            List<MemberModel> memberModels = new List<MemberModel>();

            foreach (Member member in clubmembershipDataContext.Members)
            {
                memberModels.Add(MapDbObjectToModel(member));
            }
            return memberModels;
        }
        public MemberModel GetMemberByID(Guid id)
        {
            MemberModel memberModel = new MemberModel();

            Member member = clubmembershipDataContext.Members.FirstOrDefault(x => x.IDMember == id);

            return MapDbObjectToModel(member);
        }

        public void InsertMember(MemberModel memberModel)
        {
            memberModel.IDMember = Guid.NewGuid();

            clubmembershipDataContext.Members.InsertOnSubmit(MapModelToDbObject(memberModel));
            clubmembershipDataContext.SubmitChanges();
        }
        
        public void UpdateMember(MemberModel memberModel)
        {
            Member existingMember = clubmembershipDataContext.Members.FirstOrDefault
                (x => x.IDMember == memberModel.IDMember);
            if (existingMember != null)
            {
                existingMember.IDMember = memberModel.IDMember;
                existingMember.Name = memberModel.Name;
                existingMember.Position = memberModel.Position;
                existingMember.Title = memberModel.Title;
                existingMember.Description = memberModel.Description;
                existingMember.Resume = memberModel.Resume;
                
                clubmembershipDataContext.SubmitChanges();
            }
       
    }
        public void DeleteMember(Guid ID)
        {
            Models.DBObjects.Member memberToDelete = clubmembershipDataContext.Members.FirstOrDefault
                (x => x.IDMember == ID);
            if (memberToDelete != null)
            {
                clubmembershipDataContext.Members.DeleteOnSubmit(memberToDelete);
                clubmembershipDataContext.SubmitChanges();
            }
        }

        private MemberModel MapDbObjectToModel(Member member)
        {
            MemberModel memberModel = new MemberModel();
            if (member != null)
            {
                memberModel.IDMember = member.IDMember;
                memberModel.Name = member.Name;
                memberModel.Position = member.Position;
                memberModel.Description = member.Description;
                memberModel.Title = member.Title;
                memberModel.Resume = member.Resume;
                

                return memberModel;
            }
            return null;
        }
        private Member MapModelToDbObject(MemberModel memberModel)
        {
            Member dbMemberModel = new Member();
            if (memberModel != null)
            {
                
                dbMemberModel.IDMember = memberModel.IDMember;
                dbMemberModel.Name = memberModel.Name;
                dbMemberModel.Position = memberModel.Position;
                dbMemberModel.Title = memberModel.Title;
                dbMemberModel.Description = memberModel.Description;
                dbMemberModel.Resume = memberModel.Resume;
                
                return dbMemberModel;
            }
            return null;
        }

    }
}