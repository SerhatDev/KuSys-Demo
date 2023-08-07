using KuSys.Core.Constants;
using KuSys.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KuSys.DataAccess;

/// <summary>
/// Extension methods for Database operations.
/// </summary>
public static class DatabaseExtensions
{
    /// <summary>
    /// Seed default courses.
    /// </summary>
    /// <param name="builder">ModelBuilder</param>
    /// <returns><see cref="ModelBuilder"/></returns>
    public static ModelBuilder SeedCourses(this ModelBuilder builder)
    {
        // Get the course entity set
        var courseBuilder = builder.Entity<Course>();
        
        // Add new Data
        courseBuilder.HasData(new Course()
        {
            Name = "Introduction to Computer Science",
            Code = "CSI101",
            Id = Guid.NewGuid()
        });
        
        // Add new Data
        courseBuilder.HasData(new Course()
        {
            Name = "Algorithms",
            Code = "CSI102",
            Id = Guid.NewGuid()
        });
        
        // Add new Data
        courseBuilder.HasData(new Course()
        {
            Name = "Calculus",
            Code = "MAT101",
            Id = Guid.NewGuid()
        });
        
        // Add new Data
        courseBuilder.HasData(new Course()
        {
            Name = "Physics",
            Code = "PHY101",
            Id = Guid.NewGuid()
        });
        
        // Return builder
        return builder;
    }

    /// <summary>
    /// Seed default roles.
    /// </summary>
    /// <param name="modelBuilder">ModelBuilder</param>
    /// <returns></returns>
    public static ModelBuilder SeedRoles(this ModelBuilder modelBuilder)
    {
        Guid adminRoleId = Guid.NewGuid();
        Guid studentRoleId = Guid.NewGuid();
        
        // Add roles
        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { Id = adminRoleId, Name = DefaultRoles.Admin, NormalizedName = DefaultRoles.Admin.ToUpper() },
            new UserRole { Id = studentRoleId, Name = DefaultRoles.Student, NormalizedName = DefaultRoles.Student.ToUpper() }
        );

        // Get ever roles claims
        var roleClaims = 
            DefaultRoles.AdminClaims(adminRoleId)
                .Concat(DefaultRoles.StudentClaims(studentRoleId));
        
        // Seed claims for each role
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(
            roleClaims
        );
        return modelBuilder;
    }
}