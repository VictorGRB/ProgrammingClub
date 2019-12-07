using ProgrammingClubM.Models;
using ProgrammingClubM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgrammingClubM.Controllers
{
    public class AnnouncementController : Controller
    {

        private AnnouncementRepository announcementRepository = new AnnouncementRepository();



        // GET: Announcement
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<AnnouncementModel> announcements = announcementRepository.GetAllAnnouncements();
            return View("Index",announcements);
        }
        [AllowAnonymous]
        // GET: Announcement/Details/5
        public ActionResult Details(Guid id)
        {
            Models.AnnouncementModel announcementModel = announcementRepository.GetAnnouncementByID(id);
            return View("Details",announcementModel);
        }
        [Authorize(Roles ="Admin,User")]
        // GET: Announcement/Create
        public ActionResult Create()
        {
            return View("CreateAnnouncement");
        }
        [Authorize(Roles = "Admin,User")]
        // POST: Announcement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                AnnouncementModel announcementModel = new AnnouncementModel();

                UpdateModel(announcementModel);
                if (User.Identity.IsAuthenticated)
                {
                    announcementModel.Title = User.Identity.Name + ":" + announcementModel.Title;
                    announcementModel.Tags = announcementModel.Tags + "," + User.Identity.Name;
                }

                announcementRepository.InsertAnnouncement(announcementModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateAnnouncement");
            }
        }
        [Authorize(Roles = "Admin,User")]
        // GET: Announcement/Edit/5
        public ActionResult Edit(Guid id)
        {
            AnnouncementModel announcementToEdit = new AnnouncementModel();
            announcementToEdit= announcementRepository.GetAnnouncementByID(id);
            return View("Edit",announcementToEdit);
        }
        [Authorize(Roles = "Admin,User")]
        // POST: Announcement/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                AnnouncementModel announcementModel = new AnnouncementModel();

                UpdateModel(announcementModel);

                announcementRepository.UpdateAnnouncement(announcementModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: Announcement/Delete/5
        public ActionResult Delete(Guid id)
        {
            AnnouncementModel announcement = announcementRepository.GetAnnouncementByID(id);
            return View("Delete",announcement);
        }
        [Authorize(Roles = "Admin")]
        // POST: Announcement/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                announcementRepository.DeleteAnnouncement(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
