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
    [Authorize(Roles = "Teacher")]
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions?TestID=5
        public ActionResult Index(int? TestID)
        {
            if (TestID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Find(TestID);
            if (test == null)
            {
                return HttpNotFound();
            }

            ViewBag.TestID = test.Id_testu;
            ViewBag.TestStatus = test.Status;
            var questions = db.Questions.Where(p => p.Test.Id_testu == test.Id_testu);

            return PartialView("_Index", questions.ToList() );
        }

        //GET: Questions/Create?TestID=5
        public ActionResult Create(int? TestID)
        {
            if (TestID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Find(TestID);
            if (test == null)
            {
                return HttpNotFound();
            }
            if (test.Status == "Open")
            {
                TempData["msg"] = "Nie można dodać pytania do testu, który jest otwarty do wypełniania!";
                TempData["option"] = "warning";
                return RedirectToAction("Details", "Tests", new { id = test.Id_testu });
            }

            CreateQuestionViewModels question = new CreateQuestionViewModels();
            question.TestID = test.Id_testu;           
            return PartialView("_Create",question);
        }

        //Post: Questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateQuestionViewModels question)
        {
            if (ModelState.IsValid)
            {
                Test test = db.Tests.Single(p => p.Id_testu == question.TestID);
                var quest = new Question();
                quest.Content = question.Content;
                quest.Test = test;

                db.Questions.Add(quest);
                db.SaveChanges();

                TempData["msg"] = "Utworzono nowe pytanie do testu poprawnie!";
                TempData["option"] = "success";
                return RedirectToAction("Details","Tests", new { id = test.Id_testu });
            }

            return PartialView("_Create", question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            if (question.Test.Status == "Open")
            {
                TempData["msg"] = "Nie można usunąć pytania z testu, który jest otwarty do wypełniania!";
                TempData["option"] = "warning";
                return RedirectToAction("Details", "Tests", new { id = question.Test.Id_testu });
            }

            return PartialView("_Delete",question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            int TestID = question.Test.Id_testu;
            db.Questions.Remove(question);
            db.SaveChanges();

            TempData["msg"] = "Usunięto pytanie z testu poprawnie!";
            TempData["option"] = "success";
            return RedirectToAction("Details","Tests",new {id = TestID });
        }


        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Question question = db.Questions.Find(id);

            if (question == null)
            {
                return HttpNotFound();
            }
            if (question.Test.Status == "Open")
            {
                TempData["msg"] = "Nie można edytować pytania w teście, który jest otwarty do wypełniania!";
                TempData["option"] = "warning";
                return RedirectToAction("Details", "Tests", new { id = question.Test.Id_testu });
            }

            EditQuestionViewModels ques = new EditQuestionViewModels();
            ques.QuestionID = question.Id_question;
            ques.TestID = question.Test.Id_testu;
            ques.Content = question.Content;
   
            return PartialView("_Edit", ques);
        }

        // POST: Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditQuestionViewModels ques)
        {
       
            if (ModelState.IsValid)
            {
                Question question = db.Questions.Find(ques.QuestionID);
                question.Content = ques.Content;

                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();

                TempData["msg"] = "Edytowano pytanie w teście poprawnie!";
                TempData["option"] = "success";
                return RedirectToAction("Details","Tests",new { id = ques.TestID });
            }
            return PartialView("_Edit",ques);
        }

    }
}