using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingClubM.Models;
using ProgrammingClubM.Models.DBObjects;
using ProgrammingClubM.Repository;
using System.Configuration;

namespace ProgrammingClubM.Tests.Repository
{
    [TestClass]
    public class AnnouncementRepositoryTest

    {
        private Models.DBObjects.ClubMembershipModelsDataContext clubmembershipDataContext;
        private string testDbConnectionString;
        private AnnouncementRepository announcementResource;

        [TestInitialize]
        public void Initialize()
        {
            testDbConnectionString = ConfigurationManager.ConnectionStrings["ProgrammingClubM.Tests.Properties.Settings.clubmembershiptestsConnectionString"].ConnectionString;
            clubmembershipDataContext = new Models.DBObjects.ClubMembershipModelsDataContext(testDbConnectionString);
            announcementResource = new AnnouncementRepository(clubmembershipDataContext);
        }
        [TestCleanup]
        public void TestCleanup()
        {
            clubmembershipDataContext.Announcements.DeleteAllOnSubmit(clubmembershipDataContext.Announcements);
            clubmembershipDataContext.SubmitChanges();
        }
        [TestMethod]
        public void GetAllAnnouncements_TwoRecordsExists()
        {
            Announcement announcement1 = new Announcement
            {
                IDAnnouncement = Guid.NewGuid(),
                ValidFrom = new DateTime(2018, 10, 1),
                ValidTo = new DateTime(2018, 11, 15),
                EventDateTime = new DateTime(2018, 11, 1),
                Tags = "tags1",
                Text = "Eventcall1",
                Title = "Event1"
            };
            Announcement announcement2 = new Announcement
            {
                IDAnnouncement = Guid.NewGuid(),
                ValidFrom = new DateTime(2017, 1, 1),
                ValidTo = new DateTime(2017, 3, 1),
                EventDateTime = null,
                Tags = "tags",
                Text = "Announcement",
                Title = "Important"

            };
            clubmembershipDataContext.Announcements.InsertOnSubmit(announcement1);
            clubmembershipDataContext.Announcements.InsertOnSubmit(announcement2);
            clubmembershipDataContext.SubmitChanges();

            List<AnnouncementModel> result = announcementResource.GetAllAnnouncements();
            Assert.AreEqual(2, result.Count);

            AnnouncementModel announcementModel1 = result.FirstOrDefault(x => x.IDAnnouncement == announcement1.IDAnnouncement);
            Assert.AreEqual(announcementModel1, announcement1);
            AnnouncementModel announcementModel2 = result.FirstOrDefault(x => x.IDAnnouncement == announcement2.IDAnnouncement);
            Assert.AreEqual(announcementModel2, announcement2);
        }
        [TestMethod]
        public void GetAnnouncementById_AnnouncementExists()
        {
            Guid ID = Guid.NewGuid();
            Announcement expectedAnnouncement = new Announcement
            {
                IDAnnouncement = ID,
                ValidFrom = new DateTime(2018, 10, 1),
                ValidTo = new DateTime(2018, 11, 15),
                EventDateTime = new DateTime(2018, 11, 1),
                Tags = "tags1",
                Text = "Eventcall1",
                Title = "Event1"
            };
            Announcement announcement2 = new Announcement
            {
                IDAnnouncement = Guid.NewGuid(),
                ValidFrom = new DateTime(2017, 1, 1),
                ValidTo = new DateTime(2017, 3, 1),
                EventDateTime = null,
                Tags = "tags",
                Text = "Announcement",
                Title = "Important"
            };
            clubmembershipDataContext.Announcements.InsertOnSubmit(expectedAnnouncement);
            clubmembershipDataContext.Announcements.InsertOnSubmit(announcement2);
            clubmembershipDataContext.SubmitChanges();

            AnnouncementModel result = announcementResource.GetAnnouncementByID(ID);
            Assert.AreEqual(result, expectedAnnouncement);
        }
    }
}
