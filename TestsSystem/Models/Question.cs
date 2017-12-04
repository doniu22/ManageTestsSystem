using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestsSystem.Models
{
    public class Question
    {
        public Question()
        {
            this.PossibleAnswers = new HashSet<PossibleAnswer>();
        }

        [Key]
        public int Id_question { get; set; }
        public string Content  { get; set; }

        public virtual Test Test { get; set; }
        public virtual ICollection<PossibleAnswer> PossibleAnswers { get; set; }
        public virtual ICollection<Answer>  Answers { get; set; }

    }
}