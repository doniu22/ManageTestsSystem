using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        // GET: Users
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Name = model.Name, Surname = model.Surname, UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, model.RoleName);

                    TempData["msg"] = "Dodano nowego użytkownika";
                    TempData["option"] = "success";
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

            EditViewModel model = new EditViewModel();
            model.Id = applicationUser.Id;
            model.Name = applicationUser.Name;
            model.Surname = applicationUser.Surname;
            model.Email = applicationUser.Email;
           
            
            return View(model);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = db.Users.Find(model.Id);
                applicationUser.Name = model.Name;
                applicationUser.Surname = model.Surname;
                applicationUser.Email = model.Email;
                applicationUser.UserName = model.Email;

                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();

                TempData["msg"] = "Edytowano dane poprawnie";
                TempData["option"] = "success";
                if (User.IsInRole("Admin"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Index","Home");
            }
            return View(model);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
           // db.Tests.Where(p => p.Owner.Id == applicationUser.Id).Load(); - do zmian aby testy które utworzył zostały a ich twórca był na null
            db.Users.Remove(applicationUser);
            db.SaveChanges();

            TempData["msg"] = "Usunięto użytkownika";
            TempData["option"] = "error";
            return RedirectToAction("Index");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [Authorize(Roles = "Teacher,User")]
        public ActionResult EditMyAccount()
        {
            ApplicationUser user = db.Users.Single(p => p.UserName == User.Identity.Name);
            return RedirectToAction("Edit", new { id=user.Id});
        }

    }
}