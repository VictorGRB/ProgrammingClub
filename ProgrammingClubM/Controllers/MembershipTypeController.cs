using ProgrammingClubM.Models;
using ProgrammingClubM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgrammingClubM.Controllers
{
    
    public class MembershipTypeController : Controller
    {
        private Repository.MembershipTypeRepository membershipTypeRepository = new Repository.MembershipTypeRepository();
        private MembershipRepository membershipsRepository = new MembershipRepository();
        // GET: MembershipType
        public ActionResult Index()
        {
            List<Models.MembershipTypeModel> membershipTypeModel = membershipTypeRepository.GetAllMembershipTypes();
            return View("Index",membershipTypeModel);
        }

        // GET: MembershipType/Details/5
        public ActionResult Details(Guid id)
        {
            Models.MembershipTypeModel membershipTypeModel = membershipTypeRepository.GetMembershipTypeByID(id);
            return View("Details",membershipTypeModel);
        }

        // GET: MembershipType/Create
        public ActionResult Create()
        {
            return View("CreateMembershipType");
        }

        // POST: MembershipType/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Models.MembershipTypeModel membershipTypeModel = new Models.MembershipTypeModel();
                UpdateModel(membershipTypeModel);
                membershipTypeRepository.InsertMembershipType(membershipTypeModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateMembershipType");
            }
        }

        // GET: MembershipType/Edit/5
        public ActionResult Edit(Guid id)
        {
            Models.MembershipTypeModel membershipTypeModel = membershipTypeRepository.GetMembershipTypeByID(id);

            return View("Edit",membershipTypeModel);
        }

        // POST: MembershipType/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Models.MembershipTypeModel membershipTypeModel = new Models.MembershipTypeModel();
                UpdateModel(membershipTypeModel);
                membershipTypeRepository.UpdateMembershipType(membershipTypeModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        // GET: MembershipType/Delete/5
        public ActionResult Delete(Guid id)
        {
            Models.MembershipTypeModel membershipTypeModel = membershipTypeRepository.GetMembershipTypeByID(id);
            return View("Delete",membershipTypeModel);
        }
        
        // POST: MembershipType/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                List<MembershipModel> memberships = membershipsRepository.GetAllMembershipsByMembershipTypeId(id);
                foreach(MembershipModel membership in memberships)
                {
                    membershipsRepository.DeleteMembership(membership.IDMembership);
                }
                membershipTypeRepository.DeleteMembershipType(id);

                return RedirectToAction("Index");

            }
            catch
            {
                return View("Delete");
            }
        }

    }
}
