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
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        public ActionResult Index(int TestID)
        {
            ViewBag.TestID = TestID;
            var questions = db.Questions.Where(p => p.Test.Id_testu == TestID);
            return PartialView("_Index", questions.ToList() );

        }

        //GET: Questions/Create
        public ActionResult Create(int TestID)
        {
            CreateQuestionViewModels question = new CreateQuestionViewModels();
            question.TestID = TestID;           
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

            EditQuestionViewModels ques = new EditQuestionViewModels();
            ques.QuestionID = question.Id_question;
            ques.TestID = question.Test.Id_testu;
            ques.Content = question.Content;
   
            return PartialView("_Edit", ques);
        }

        // POST: Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditQuestionViewModels ques )
        {
       
            if (ModelState.IsValid)
            {
                Question question = db.Questions.Find(ques.QuestionID);
                question.Content = ques.Content;

                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details","Tests",new { id = ques.TestID });
            }
            return PartialView("_Edit",ques);
        }

    }
}