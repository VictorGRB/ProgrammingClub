using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProgrammingClubM.Models;
using ProgrammingClubM.Models.DBObjects;

namespace ProgrammingClubM.Repository
{
    public class MembershipTypeRepository
    {
        private ClubMembershipModelsDataContext clubmembershipDataContext;

        public MembershipTypeRepository()
        {
            this.clubmembershipDataContext = new ClubMembershipModelsDataContext();
        }
        
        public List<MembershipTypeModel> GetAllMembershipTypes()
        {
            List<MembershipTypeModel> membershipTypeModels = new List<MembershipTypeModel>();

            foreach (MembershipType membershipType in clubmembershipDataContext.MembershipTypes)
            {
                membershipTypeModels.Add(MapDbObjectToModel(membershipType));
            }
            return membershipTypeModels;
        }
        public MembershipTypeModel GetMembershipTypeByID(Guid id)
        {
            MembershipTypeModel membershipTypeModel = new MembershipTypeModel();

            MembershipType membershipType = clubmembershipDataContext.MembershipTypes.FirstOrDefault(x => x.IDMembershipType == id);

            return MapDbObjectToModel(membershipType);
        }

        //public List<MembershipTypeModel> GetAnnouncementsByEffectiveDates(DateTime validFrom, DateTime validTo)
        //{
        //    List<MembershipTypeModel> membershipTypeModels = new List<MembershipTypeModel>();
        //    foreach (MembershipType membershipType in clubmembershipDataContext.MembershipTypes.Where
        //        (x => x.ValidFrom >= validFrom && x.ValidTo <= validTo))
        //    {
        //        membershipTypeModels.Add(MapDbObjectToModel(membershipType));
        //    }
        //    return membershipTypeModels;
        //}

        //public List<MembershipTypeModel> GetAnnouncementsByEventDate(DateTime eventDate)
        //{
        //    List<MembershipTypeModel> membershipTypeModels = new List<MembershipTypeModel>();

        //    foreach (MembershipType membershipType in clubmembershipDataContext.MembershipTypes.Where
        //        (x => x.EventDateTime.HasValue && x.EventDateTime.Value.Date == eventDate.Date))
        //    {
        //        membershipTypeModels.Add(MapDbObjectToModel(membershipType));
        //    }
        //    return membershipTypeModels;
        //}

        public void InsertMembershipType(MembershipTypeModel membershipTypeModel)
        {
            membershipTypeModel.IDMembershipType = Guid.NewGuid();

            clubmembershipDataContext.MembershipTypes.InsertOnSubmit(MapModelToDbObject(membershipTypeModel));
            clubmembershipDataContext.SubmitChanges();
        }
        
        public void UpdateMembershipType(MembershipTypeModel membershipTypeModel)
        {
            Models.DBObjects.MembershipType existingAnnouncement = clubmembershipDataContext.MembershipTypes.FirstOrDefault
                (x => x.IDMembershipType == membershipTypeModel.IDMembershipType);
            if (existingAnnouncement != null)
            {
                existingAnnouncement.IDMembershipType = membershipTypeModel.IDMembershipType;
                existingAnnouncement.Name = membershipTypeModel.Name;
                existingAnnouncement.Description = membershipTypeModel.Description;
                existingAnnouncement.SubscriptionLengthInMonths = membershipTypeModel.SubscriptionLengthInMonths;
                
                clubmembershipDataContext.SubmitChanges();
            }
        }
        public void DeleteMembershipType(Guid ID)
        {
            Models.DBObjects.MembershipType membershipTypeToDelete = clubmembershipDataContext.MembershipTypes.FirstOrDefault
                (x => x.IDMembershipType == ID);
            if (membershipTypeToDelete != null)
            {
                clubmembershipDataContext.MembershipTypes.DeleteOnSubmit(membershipTypeToDelete);
                clubmembershipDataContext.SubmitChanges();
            }
        }

        private MembershipTypeModel MapDbObjectToModel(MembershipType membershipType)
        {
            MembershipTypeModel membershipTypeModel = new MembershipTypeModel();
            if (membershipType != null)
            {
                membershipTypeModel.IDMembershipType = membershipType.IDMembershipType;
                membershipTypeModel.Name = membershipType.Name;
                membershipTypeModel.Description = membershipType.Description;
                membershipTypeModel.SubscriptionLengthInMonths = membershipType.SubscriptionLengthInMonths;
                

                return membershipTypeModel;
            }
            return null;
        }
        private MembershipType MapModelToDbObject(MembershipTypeModel membershipTypeModel)
        {
            MembershipType dbMembershipTypeModel = new MembershipType();
            if (membershipTypeModel != null)
            {
                dbMembershipTypeModel.IDMembershipType = membershipTypeModel.IDMembershipType;
                dbMembershipTypeModel.Name = membershipTypeModel.Name;
                dbMembershipTypeModel.Description = membershipTypeModel.Description;
                dbMembershipTypeModel.SubscriptionLengthInMonths = membershipTypeModel.SubscriptionLengthInMonths;
                
                return dbMembershipTypeModel;
            }
            return null;
        }
    }
}
