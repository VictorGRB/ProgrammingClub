using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProgrammingClubM.Models;
using ProgrammingClubM.Models.DBObjects;

namespace ProgrammingClubM.Repository
{
    public class AnnouncementRepository
    {
        private ClubMembershipModelsDataContext clubmembershipDataContext;

        public AnnouncementRepository()
        {
            this.clubmembershipDataContext = new ClubMembershipModelsDataContext();
        }

        public AnnouncementRepository(ClubMembershipModelsDataContext clubmembershipDataContext)
        {
            this.clubmembershipDataContext = clubmembershipDataContext;
        }

        public List<AnnouncementModel> GetAllAnnouncements()
        {
            List<AnnouncementModel> announcementModels = new List<AnnouncementModel>();

            foreach (Announcement announcement in clubmembershipDataContext.Announcements)
            {
                announcementModels.Add(MapDbObjectToModel(announcement));
            }
            return announcementModels;
        }
        public AnnouncementModel GetAnnouncementByID(Guid id)
        {
            AnnouncementModel announcementModel = new AnnouncementModel();

            Announcement announcement = clubmembershipDataContext.Announcements.FirstOrDefault(x => x.IDAnnouncement == id);

            return MapDbObjectToModel(announcement);
        }

        public List<AnnouncementModel> GetAnnouncementsByEffectiveDates(DateTime validFrom, DateTime validTo)
        {
            List<AnnouncementModel> announcementModels = new List<AnnouncementModel>();
            foreach (Announcement announcement in clubmembershipDataContext.Announcements.Where
                (x => x.ValidFrom >= validFrom && x.ValidTo <= validTo))
            {
                announcementModels.Add(MapDbObjectToModel(announcement));
            }
            return announcementModels;
        }

        public List<AnnouncementModel> GetAnnouncementsByEventDate(DateTime eventDate)
        {
            List<AnnouncementModel> announcementModels = new List<AnnouncementModel>();

            foreach (Announcement announcement in clubmembershipDataContext.Announcements.Where
                (x => x.EventDateTime.HasValue && x.EventDateTime.Value.Date == eventDate.Date))
            {
                announcementModels.Add(MapDbObjectToModel(announcement));
            }
            return announcementModels;
        }
        
        public void InsertAnnouncement(AnnouncementModel announcementModel)
        {
            announcementModel.IDAnnouncement = Guid.NewGuid();

            clubmembershipDataContext.Announcements.InsertOnSubmit(MapModelToDbObject(announcementModel));
            clubmembershipDataContext.SubmitChanges();
        }

        public void UpdateAnnouncement(AnnouncementModel announcementModel)
        {
            Models.DBObjects.Announcement existingAnnouncement = clubmembershipDataContext.Announcements.FirstOrDefault
                (x => x.IDAnnouncement == announcementModel.IDAnnouncement);
            if (existingAnnouncement != null)
            {
                existingAnnouncement.IDAnnouncement = announcementModel.IDAnnouncement;
                existingAnnouncement.ValidFrom = announcementModel.ValidFrom;
                existingAnnouncement.ValidTo = announcementModel.ValidTo;
                existingAnnouncement.Title = announcementModel.Title;
                existingAnnouncement.Text = announcementModel.Text;
                existingAnnouncement.EventDateTime = announcementModel.EventDateTime;
                existingAnnouncement.Tags = announcementModel.Tags;
                clubmembershipDataContext.SubmitChanges();
            }
        }
        public void DeleteAnnouncement(Guid ID)
        {
            Models.DBObjects.Announcement announcementToDelete = clubmembershipDataContext.Announcements.FirstOrDefault
                (x => x.IDAnnouncement == ID);
            if (announcementToDelete != null)
            {
                clubmembershipDataContext.Announcements.DeleteOnSubmit(announcementToDelete);
                clubmembershipDataContext.SubmitChanges();
            }
        }

        private AnnouncementModel MapDbObjectToModel(Announcement announcement)
        {
            AnnouncementModel announcementModel = new AnnouncementModel();
            if (announcement != null)
            {
                announcementModel.IDAnnouncement = announcement.IDAnnouncement;
                announcementModel.EventDateTime = announcement.EventDateTime;
                announcementModel.Tags = announcement.Tags;
                announcementModel.Text = announcement.Text;
                announcementModel.Title = announcement.Title;
                announcementModel.ValidFrom = announcement.ValidFrom;
                announcementModel.ValidTo = announcement.ValidTo;

                return announcementModel;
            }
            return null;
        }
        private Announcement MapModelToDbObject(AnnouncementModel announcementModel)
        {
            Announcement dbAnnouncementModel = new Announcement();
            if (announcementModel != null)
            {
                dbAnnouncementModel.IDAnnouncement = announcementModel.IDAnnouncement;
                dbAnnouncementModel.ValidFrom = announcementModel.ValidFrom;
                dbAnnouncementModel.ValidTo = announcementModel.ValidTo;
                dbAnnouncementModel.Title = announcementModel.Title;
                dbAnnouncementModel.Text = announcementModel.Text;
                dbAnnouncementModel.EventDateTime = announcementModel.EventDateTime;
                dbAnnouncementModel.Tags = announcementModel.Tags;
                return dbAnnouncementModel;
            }
            return null;



        }




    }
}
