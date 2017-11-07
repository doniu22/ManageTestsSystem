using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestsSystem.Models
{
    public class CreateQuestionViewModels
    {
        public int TestID { get; set; }
        public string Content { get; set; }
    }

    public class EditQuestionViewModels
    {
        public int QuestionID { get; set; }
        public int TestID { get; set; }
        public string Content { get; set; }
    }
}