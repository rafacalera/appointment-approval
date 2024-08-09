using Microsoft.EntityFrameworkCore;
using TimeEntryApproval.API.Domain;

namespace TimeEntryApproval.API.Infrastructure
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options) { }

        DbSet<TimeEntry> TimeEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeEntry>().ToTable("TimeEntries");
            modelBuilder.Entity<TimeEntry>().Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<TimeEntry>().Property(x => x.Date)
                .IsRequired();
            modelBuilder.Entity<TimeEntry>().Property(x => x.Start).IsRequired();
            modelBuilder.Entity<TimeEntry>().Property(x => x.End).IsRequired();
            modelBuilder.Entity<TimeEntry>().Property(x => x.TaskId).IsRequired();
            modelBuilder.Entity<TimeEntry>().Property(x => x.UserId).IsRequired();
            modelBuilder.Entity<TimeEntry>().Property(x => x.IsApproved).IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
