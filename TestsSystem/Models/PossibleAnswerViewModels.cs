using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestsSystem.Models
{
    public class CreatePossibleAnswerViewModels
    {
        public int TestID { get; set; }
        public int QuestionID { get; set; }
        public string Content { get; set; }
        public string isCorrect { get; set; }
    }

    public class EditPossibleAnswerViewModels
    {
        public int PosAnsID { get; set; }
        public int TestID { get; set; }
        public string Content { get; set; }
        public string isCorrect { get; set; }
    }
}