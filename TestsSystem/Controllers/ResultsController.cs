using System;
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
    public class ResultsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Results
        [Authorize(Roles = "User")]
        public ActionResult MyResults()
        {
            var user = db.Users.Single(p => p.UserName == User.Identity.Name);
            var results = db.Results.Where(p => p.Owner.Id == user.Id);

            return View("Index",results.ToList());
        }

        // GET: Results?TestID = 5
        [Authorize(Roles = "Teacher")]
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
            var results = db.Results.Where(p => p.Test.Id_testu == test.Id_testu);

            return View(results.ToList());
        }

        //GET: Results/Create?TestID=5
        [Authorize(Roles = "User")]
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
            if (test.Status == "Prepared")
            {
                TempData["msg"] = "Nie można wypełnić testu, który jest przygotowywany!";
                TempData["option"] = "warning";
                return RedirectToAction("Index", "Tests");
            }

            CreateResultViewModels result = new CreateResultViewModels();
            result.TestID = test.Id_testu;
            result.TestTitle = test.Title;
            result.TestSubject = test.Subject;
            result.TestCreator = test.Created_by;
            result.TestData = test.Created_at;
            result.TestQuestions = test.Questions.ToList();
            ViewBag.QuestionsCount = result.TestQuestions.Count;

            //utworzenie listy obiektów na odpowiedzi do testu
            var answersList = new List<AnswerViewModels>();
            for(int i=0; i< result.TestQuestions.Count;i++)
            {
                var answer = new AnswerViewModels();
                answer.Id_question = result.TestQuestions[i].Id_question;
                answer.Content = " ";
                answersList.Add(answer);
            }

            result.AnswersViewModels = answersList;

            return View(result);
        }

        //Post: Results/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Create(CreateResultViewModels result)
        {
            if (ModelState.IsValid)
            {
                var test = db.Tests.Single(p => p.Id_testu == result.TestID);
                var user = db.Users.Single(p => p.UserName == User.Identity.Name);

                //utworzenie nowego wypełnienia i dodanie do bazy
                var res = new Result();
                res.Created_By = user.Name + ' ' + user.Surname;
                res.Created_At = System.DateTime.Now.ToString();
                res.Owner = user;
                res.Test = test;

                db.Results.Add(res);

                //dodanie do bazy odpowiedzi tego wypelnienia
                for (int i = 0; i < result.AnswersViewModels.Count; i++)
                {
                    var id_question = result.AnswersViewModels[i].Id_question;
                    var question = db.Questions.Single(p => p.Id_question == id_question );
                    var answer = new Answer();
                    answer.Content = result.AnswersViewModels[i].Content;    
                    answer.Question = question;
                    answer.Result = res;

                    db.Answers.Add(answer);
                }


                db.SaveChanges();


                return RedirectToAction("Details", "Results", new {id=res.Id_result });
            }

            return View(result);
        }

        // GET: Results/Details/5
        [Authorize(Roles = "User, Teacher")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }

            // zliczenie ilości poprawnych odpowiedzi
            int ScoredPoints = 0;

            foreach (var answer in result.Answers)
            {
                foreach (var pos_ans in answer.Question.PossibleAnswers)
                {
                    if (answer.Content == pos_ans.Content && pos_ans.isCorrect == "True")
                        ScoredPoints++;
                }
            }

            ViewBag.ScoredPoints = ScoredPoints;

            return View(result);
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
