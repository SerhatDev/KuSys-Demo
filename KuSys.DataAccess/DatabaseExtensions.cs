using KuSys.Entities;
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
}