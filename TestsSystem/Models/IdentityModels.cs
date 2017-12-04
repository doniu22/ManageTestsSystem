using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TestsSystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Tests = new HashSet<Test>();
        }

        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<Result> Results { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("TestsSystemDB", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // gałąź tworzenia testu
            modelBuilder.Entity<Test>()
            .HasOptional(b => b.Owner)
            .WithMany(b => b.Tests)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
            .HasRequired(b => b.Test)
            .WithMany(b => b.Questions)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<PossibleAnswer>()
            .HasRequired(b => b.Question)
            .WithMany(b => b.PossibleAnswers)
            .WillCascadeOnDelete(true);

            //gałąź wypełniania testu
            modelBuilder.Entity<Result>()
            .HasOptional(b => b.Owner)
            .WithMany(b => b.Results)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Result>()
            .HasRequired(b => b.Test)
            .WithMany(b => b.Results)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Answer>()
            .HasRequired(b => b.Result)
            .WithMany(b => b.Answers)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<Answer>()
            .HasRequired(b => b.Question)
            .WithMany(b => b.Answers)
            .WillCascadeOnDelete(false);


        }


        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<PossibleAnswer> PossibleAnswers { get; set; }

        public DbSet<Result> Results { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}