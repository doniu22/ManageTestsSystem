using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestsSystem.Models
{
    public class CreateResultViewModels
    {
        public int TestID { get; set; }
        public string TestTitle { get; set; }
        public string TestSubject { get; set; }
        public string TestCreator { get; set; }
        public string TestData { get; set; }
        public List<Question> TestQuestions { get; set; }
        public List<AnswerViewModels> AnswersViewModels { get; set; }
    }
}