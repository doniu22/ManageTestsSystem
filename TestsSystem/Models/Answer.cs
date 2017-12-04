using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestsSystem.Models
{
    public class Answer
    {
        [Key]
        public int Id_answer { get; set; }
        public string Content { get; set; }

        public virtual Question Question { get; set; }
        public virtual Result Result { get; set; }

    }
}