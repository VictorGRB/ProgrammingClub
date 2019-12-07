using ProgrammingClubM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgrammingClubM.Controllers
{
    public class MembershipController : Controller
    {
        private MembershipTypeRepository MembershipTypeRepository = new MembershipTypeRepository();
        private Repository.MembershipRepository membershipRepository = new Repository.MembershipRepository();
        private MemberRepository memberRepository = new MemberRepository();
        // GET: Membership
        public ActionResult Index()
        {
            List<Models.MembershipModel> memberships = membershipRepository.GetAllMemberships();
            return View("Index",memberships);
        }

        // GET: Membership/Details/5
        public ActionResult Details(Guid id)
        {
            Models.MembershipModel membershipModel = membershipRepository.GetMembershipByID(id);
            return View("Details",membershipModel);
        }

        // GET: Membership/Create
        public ActionResult Create()
        {
            var membershipTypes = MembershipTypeRepository.GetAllMembershipTypes();
            SelectList lst = new SelectList(membershipTypes, "IDMembershipType", "Name");
            ViewData["membershipType"]=lst;

            var members = memberRepository.GetAllMembers();
            SelectList membersList = new SelectList(members, "IDMember", "Name");
            ViewData["member"] = membersList;
            return View("CreateMembership");
        }

        // POST: Membership/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Models.MembershipModel membershipModel = new Models.MembershipModel();
                UpdateModel(membershipModel);
                membershipRepository.InsertMembership(membershipModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateMembership");
            }
        }

        // GET: Membership/Edit/5
        public ActionResult Edit(Guid id)
        {
            Models.MembershipModel membership = membershipRepository.GetMembershipByID(id);
            return View("Edit",membership);
        }

        // POST: Membership/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Models.MembershipModel membershipModel = new Models.MembershipModel();
                UpdateModel(membershipModel);
                membershipRepository.UpdateMembership(membershipModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        // GET: Membership/Delete/5
        public ActionResult Delete(Guid id)
        {
            Models.MembershipModel membershipModel = membershipRepository.GetMembershipByID(id);
            return View("Delete",membershipModel);
        }

        // POST: Membership/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                membershipRepository.DeleteMembership(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
