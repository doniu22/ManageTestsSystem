using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestsSystem.Models
{
    public class PossibleAnswer
    {
        [Key]
        public int Id_posans { get; set; }
        public string Content { get; set; }
        public string isCorrect { get; set; }

        public virtual Question Question { get; set; }
    }
}