using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestsSystem.Models
{
    public class Result
    {
        [Key]
        public int Id_result { get; set; }
        public string Created_At { get; set; }
        public string Created_By { get; set; }

        public virtual ApplicationUser Owner { get; set; }
        public virtual Test Test { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}