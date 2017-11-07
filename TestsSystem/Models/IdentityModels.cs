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

            modelBuilder.Entity<Test>()
            .HasRequired(b => b.Owner)
            .WithMany(b => b.Tests)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<Question>()
            .HasRequired(b => b.Test)
            .WithMany(b => b.Questions)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<PossibleAnswer>()
            .HasRequired(b => b.Question)
            .WithMany(b => b.PossibleAnswers)
            .WillCascadeOnDelete(true);
        }


        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<PossibleAnswer> PossibleAnswers { get; set; }
    }
}