using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProgrammingClubM.Models;
using ProgrammingClubM.Models.DBObjects;

namespace ProgrammingClubM.Repository
{
    public class MembershipRepository
    {
        private ClubMembershipModelsDataContext clubmembershipDataContext;

        public MembershipRepository()
        {
            this.clubmembershipDataContext = new ClubMembershipModelsDataContext();
        }
        public List<MembershipModel> GetAllMembershipsByMembershipTypeId(Guid id)
        {
            List<MembershipModel> membershipList = new List<MembershipModel>();
            List<Membership> membership = clubmembershipDataContext.Memberships.Where(x => x.IDMembershipType == id).ToList();
            foreach(Models.DBObjects.Membership dbMembership in membership)
            {
                MembershipModel membershipModel = new MembershipModel();
                membershipModel.IDMembership = dbMembership.IDMembership;
                membershipModel.IDMember = dbMembership.IDMember;
                membershipModel.IDMembershipType = dbMembership.IDMembershipType;
                membershipModel.StartDate = dbMembership.StartDate;
                membershipModel.EndDate = dbMembership.EndDate;
                membershipModel.Level = dbMembership.Level;

                membershipList.Add(membershipModel);
            }
            return membershipList;
        }   
        public List<MembershipModel> GetAllMemberships()
        {
            List<MembershipModel> membershipModels = new List<MembershipModel>();

            foreach (Membership membership in clubmembershipDataContext.Memberships)
            {
                membershipModels.Add(MapDbObjectToModel(membership));
            }
            return membershipModels;
        }
        public MembershipModel GetMembershipByID(Guid id)
        {
            MembershipModel membershipModels = new MembershipModel();

            Membership membership = clubmembershipDataContext.Memberships.FirstOrDefault(x => x.IDMembership == id);

            return MapDbObjectToModel(membership);
        }

        public List<MembershipModel> GetMembershipsByEffectiveDates(DateTime startDate, DateTime endDate)
        {
            List<MembershipModel> membershipModels = new List<MembershipModel>();
            foreach (Membership membership in clubmembershipDataContext.Memberships.Where
                (x => x.StartDate >= startDate && x.EndDate <= endDate))
            {
                membershipModels.Add(MapDbObjectToModel(membership));
            }
            return membershipModels;
        }

        //public List<MembershipModel> GetMembershipsByEventDate(DateTime eventDate)
        //{
        //    List<MembershipModel> membershipModels = new List<MembershipModel>();

        //    foreach (Membership membership in clubmembershipDataContext.Memberships.Where
        //        (x => x.EventDateTime.HasValue && x.EventDateTime.Value.Date == eventDate.Date))
        //    {
        //        membershipModels.Add(MapDbObjectToModel(membership));
        //    }
        //    return membershipModels;
        //}

        public void InsertMembership(MembershipModel membershipModel)
        {
            membershipModel.IDMembership = Guid.NewGuid();

            clubmembershipDataContext.Memberships.InsertOnSubmit(MapModelToDbObject(membershipModel));
            clubmembershipDataContext.SubmitChanges();
        }
        
        public void UpdateMembership(MembershipModel membershipModel)
        {
            Models.DBObjects.Membership existingMembership = clubmembershipDataContext.Memberships.FirstOrDefault
                (x => x.IDMembership == membershipModel.IDMembership);
            if (existingMembership != null)
            {
                existingMembership.IDMembership = membershipModel.IDMembership;
                existingMembership.StartDate = membershipModel.StartDate;
                existingMembership.EndDate = membershipModel.EndDate;
                existingMembership.IDMember = membershipModel.IDMember;
                existingMembership.IDMembershipType = membershipModel.IDMembershipType;
                existingMembership.Level = membershipModel.Level;
                
                clubmembershipDataContext.SubmitChanges();
            }
        }
        public void DeleteMembership(Guid ID)
        {
            Models.DBObjects.Membership membershipToDelete = clubmembershipDataContext.Memberships.FirstOrDefault
                (x => x.IDMembership == ID);
            if (membershipToDelete != null)
            {
                clubmembershipDataContext.Memberships.DeleteOnSubmit(membershipToDelete);
                clubmembershipDataContext.SubmitChanges();
            }
        }

        private MembershipModel MapDbObjectToModel(Membership membership)
        {
            MembershipModel membershipModel = new MembershipModel();
            if (membership != null)
            {
                membershipModel.IDMembership = membership.IDMembership;
                membershipModel.IDMember = membership.IDMember;
                membershipModel.IDMembershipType = membership.IDMembershipType;
                membershipModel.StartDate = membership.StartDate;
                membershipModel.EndDate = membership.EndDate;
                membershipModel.Level = membership.Level;
                

                return membershipModel;
            }
            return null;
        }
        private Membership MapModelToDbObject(MembershipModel membershipModel)
        {
            Membership dbMembershipModel = new Membership();
            if (membershipModel != null)
            {
                dbMembershipModel.IDMembership = membershipModel.IDMembership;
                dbMembershipModel.IDMember = membershipModel.IDMember;
                dbMembershipModel.IDMembershipType = membershipModel.IDMembershipType;
                dbMembershipModel.StartDate = membershipModel.StartDate;
                dbMembershipModel.EndDate = membershipModel.EndDate;
                dbMembershipModel.Level = membershipModel.Level;
                
                return dbMembershipModel;
            }
            return null;
        }
    }
}