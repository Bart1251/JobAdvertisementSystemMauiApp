using JobAdvertisementAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobAdvertisementAppAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<JobExperience> JobExperience { get; set; }
        public DbSet<JobLevel> JobLevel { get; set; }
        public DbSet<JobType> JobType { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Offer> Offer { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<TypeOfContract> TypeOfContract { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserLanguage> UserLanguage { get; set; }
        public DbSet<WorkingShift> WorkingShift { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLanguage>().HasKey(ul => new { ul.UserId, ul.LanguageId });
            modelBuilder.Entity<UserLanguage>().HasOne(u => u.User).WithMany(ul => ul.UserLanguages).HasForeignKey(u => u.UserId);
            modelBuilder.Entity<UserLanguage>().HasOne(l => l.Language).WithMany(ul => ul.UserLanguages).HasForeignKey(l => l.LanguageId);
        }
    }
}
