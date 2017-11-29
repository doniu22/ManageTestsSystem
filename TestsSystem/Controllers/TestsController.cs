﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestsSystem.Models;

namespace TestsSystem.Controllers
{
    [Authorize]
    public class TestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tests
        [Authorize(Roles = "Teacher, User")]
        public ActionResult Index()
        {
            var testsList = new List<Test>();
            if (User.IsInRole("Teacher"))
            {
                ApplicationUser user = db.Users.Single(p => p.UserName == User.Identity.Name);
                testsList = user.Tests.ToList();
            }
            else
            {
                testsList = db.Tests.ToList();
            }

            return View(testsList);
        }

        // GET: Tests/Details/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // GET: Tests/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(Test test)
        {
            ApplicationUser user = db.Users.Single(p => p.UserName == User.Identity.Name);
            test.Created_by = user.Name + ' ' + user.Surname;
            test.Created_at = DateTime.Now.ToString();
            test.Status = "Prepared";
            test.Owner = user;

            if (ModelState.IsValid)
            {
                db.Tests.Add(test);
                db.SaveChanges();
                return RedirectToAction("Details",new { id=test.Id_testu});
            }

            return View(test);
        }

        // GET: Tests/Edit/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }

            if (test.Status == "Open")
            {
              //tutaj komunikat że nie można edytować testu który jest otwart do wypełniania
              return RedirectToAction("Index");
            }

            ViewBag.Options = new List<SelectListItem> { new SelectListItem { Text = "Prepared", Value = "Prepared" , Selected = true },
                                                         new SelectListItem { Text = "Open", Value = "Open" }};

            return View(test);
        }

        // POST: Tests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit([Bind(Include = "Id_testu,Title,Subject,Created_at,Created_by,Status")] Test test)
        {
            if (ModelState.IsValid)
            {
                db.Entry(test).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(test);
        }

        // GET: Tests/Delete/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }

            if (test.Status == "Open")
            {
                //tutaj komunikat że nie można usunąć testu który jest otwarty do wypełniania
                return RedirectToAction("Index");
            }

            return View(test);
        }

        // POST: Tests/Delete/5
        [Authorize(Roles = "Teacher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Test test = db.Tests.Find(id);
            db.Tests.Remove(test);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
