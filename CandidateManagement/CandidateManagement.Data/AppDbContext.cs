using CandidateManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagement.Data;

// ------------------------------------------------------------
// AppDbContext: DbContext chính quản lý các DbSet cho hệ thống
// ------------------------------------------------------------
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<JobPosition> JobPositions => Set<JobPosition>();
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<Interview> Interviews => Set<Interview>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ---------------------------
        // Cấu hình quan hệ Application
        // ---------------------------
        modelBuilder.Entity<Application>()
            .HasOne(a => a.Candidate)
            .WithMany(c => c.Applications)
            .HasForeignKey(a => a.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Application>()
            .HasOne(a => a.JobPosition)
            .WithMany(j => j.Applications)
            .HasForeignKey(a => a.JobPositionId)
            .OnDelete(DeleteBehavior.Cascade);

        // ------------------------
        // Cấu hình quan hệ Interview
        // ------------------------
        modelBuilder.Entity<Interview>()
            .HasOne(i => i.Application)
            .WithMany(a => a.Interviews)
            .HasForeignKey(i => i.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        // -----------------
        // Seed dữ liệu Role
        // -----------------
        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = 1, Name = "Admin" },
            new Role { RoleId = 2, Name = "HR" },
            new Role { RoleId = 3, Name = "Interviewer" }
        );
    }
}

