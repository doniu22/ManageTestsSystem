﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestsSystem.Models;

namespace TestsSystem.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

        // GET: Users/Index
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Name = model.Name, Surname = model.Surname, UserName = model.Email, Email = model.Email };
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "User");
                    return RedirectToAction("Index", "Users");
                }
                AddErrors(result);
            }
            return View(model);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,UserName,Name,Surname,PasswordHash,SecurityStamp")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}