using KuSys.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KuSys.DataAccess;

public class KuSysDbContext : IdentityDbContext<User, UserRole, Guid>
{
    /// <summary>
    /// Students Table
    /// </summary>
    public DbSet<Student> Students { get; set; }
    
    /// <summary>
    /// Courses table
    /// </summary>
    public DbSet<Course> Courses { get; set; }
    
    
    /// <summary>
    /// Student Courses Table
    /// </summary>
    public DbSet<StudentCourse> StudentCourses { get; set; }

    /// <inheritdoc />
    public KuSysDbContext(DbContextOptions options) 
        :base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Set global filter to exclude soft-deleted records 
        builder.Entity<Student>().HasQueryFilter(p => !p.IsDeleted);
        
        // Set global filter to exclude soft-deleted records 
        builder.Entity<Course>().HasQueryFilter(p => !p.IsDeleted);
        
        // Set global filter to exclude soft-deleted records 
        builder.Entity<StudentCourse>().HasQueryFilter(p => !p.IsDeleted);
        
        // Seed default courses.
        builder.SeedCourses();
        
        base.OnModelCreating(builder);
    }
}