using ProgrammingClubM.Repository;
using ProgrammingClubM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgrammingClubM.Controllers
{
    public class MemberController : Controller
    {
        private MemberRepository memberRepository = new MemberRepository();
        // GET: Member
        public ActionResult Index()
        {
            List<Models.MemberModel> members = memberRepository.GetAllMembers();
            return View("Index",members);
        }

        // GET: Member/Details/5
        public ActionResult Details(Guid id)
        {
            /*Models.MemberModel memberModel = memberRepository.GetMemberByID(id);
            return View("Details",memberModel);*/

            MemberCodesnippetsViewModel viewModel = memberRepository.GetMemberCodeSnippets(id);
            return View(viewModel);
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            return View("CreateMember");
        }

        // POST: Member/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Models.MemberModel memberModel = new Models.MemberModel();
                UpdateModel(memberModel);
                memberRepository.InsertMember(memberModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateMember");
            }
        }

        // GET: Member/Edit/5
        public ActionResult Edit(Guid id)
        {
            Models.MemberModel membermodel = memberRepository.GetMemberByID(id);
            return View("Edit",membermodel);
        }

        // POST: Member/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Models.MemberModel memberModel = new Models.MemberModel();
                UpdateModel(memberModel);
                memberRepository.UpdateMember(memberModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        // GET: Member/Delete/5
        public ActionResult Delete(Guid id)
        {
            Models.MemberModel memberModel = memberRepository.GetMemberByID(id);
            return View("Delete",memberModel);
        }

        // POST: Member/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                memberRepository.DeleteMember(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
