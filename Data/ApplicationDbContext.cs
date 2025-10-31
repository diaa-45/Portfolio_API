using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portfolio_API.Models;

namespace Portfolio_API.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // definition of the tables
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectImages> ProjectImages { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<About> About { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var hasher = new PasswordHasher<string>();
            var admin = new Administrator
            {
                Id = 1,
                Name = "Super Admin",
                Email = "admin@mail.com",
                PasswordHash = hasher.HashPassword("admin@mail.com", "gplbalization@!?")
            };

            modelBuilder.Entity<Administrator>().HasData(admin);

            // Optional: configure one-to-many explicitly
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Images)
                .WithOne(pi => pi.Project)
                .HasForeignKey(pi => pi.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
