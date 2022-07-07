using Microsoft.EntityFrameworkCore;
using TestTask.Models;
namespace TestTask.DAL
{

    public class TestAppDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public TestAppDbContext(DbContextOptions<TestAppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().Navigation(x => x.Contacts).AutoInclude();
            modelBuilder.Entity<Contact>().Navigation(x => x.Account).AutoInclude();

            modelBuilder.Entity<Incident>().Navigation(x => x.Accounts).AutoInclude();
            modelBuilder.Entity<Account>().Navigation(x => x.Incident).AutoInclude();

            modelBuilder.Entity<Incident>().Property(i => i.Name).ValueGeneratedOnAdd();
            modelBuilder.Entity<Incident>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Contact>().HasIndex(c => c.Email).IsUnique();

            base.OnModelCreating(modelBuilder);

        }
    }
}
