using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestsSystem.Models
{
    public class Test
    {
        public Test()
        {
            this.Questions = new HashSet<Question>();
        }

        [Key]
        public int Id_testu { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Created_at { get; set; }
        public string Created_by { get; set; }
        public string Status { get; set; }

        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}