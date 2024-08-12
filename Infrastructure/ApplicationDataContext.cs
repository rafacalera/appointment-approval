using Microsoft.EntityFrameworkCore;
using TimeEntryApproval.API.Domain;

namespace TimeEntryApproval.API.Infrastructure
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options) { }

        DbSet<TimeEntry> TimeEntries { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<ProjectTask> ProjectTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeEntry>().ToTable("TimeEntries");
            modelBuilder.Entity<TimeEntry>().Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<TimeEntry>().Property(x => x.Date)
                .IsRequired();
            modelBuilder.Entity<TimeEntry>().Property(x => x.Start).IsRequired();
            modelBuilder.Entity<TimeEntry>().Property(x => x.End).IsRequired();
            modelBuilder.Entity<TimeEntry>().HasOne(x => x.Task).WithMany().HasForeignKey(x => x.TaskId);
            modelBuilder.Entity<TimeEntry>().Property(x => x.UserId).IsRequired();
            modelBuilder.Entity<TimeEntry>().Property(x => x.IsApproved).IsRequired(false);

            modelBuilder.Entity<ProjectTask>().ToTable("ProjectTasks");
            modelBuilder.Entity<ProjectTask>().Property(x => x.Id).
                ValueGeneratedOnAdd();
            modelBuilder.Entity<ProjectTask>().HasOne(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId);

            modelBuilder.Entity<Project>().ToTable("Projects");
            modelBuilder.Entity<Project>().Property(x => x.Id).
                ValueGeneratedOnAdd();
            modelBuilder.Entity<Project>().Property(x => x.ProjectTypeId).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
