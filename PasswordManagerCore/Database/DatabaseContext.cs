using Microsoft.EntityFrameworkCore;
using PasswordManagerCore.Model;

namespace PasswordManagerCore.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base()
        {
            Database.EnsureCreated();
        }

        public DbSet<SignInInformation> SignInInformations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SignInInformation>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=sqlitedb.db");
        }
    }
}
