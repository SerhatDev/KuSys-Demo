using System.ComponentModel.DataAnnotations;

namespace KuSys.Entities;

/// <summary>
/// Course entity.
/// </summary>
public sealed class Course : EntityBase<Guid>
{
    /// <summary>
    /// Course code. 
    /// </summary>
    [MaxLength(12)]
    public string Code { get; set; }
    
    
    /// <summary>
    /// Course Name
    /// </summary>
    [MaxLength(50)]
    public string Name { get; set; }
    
    /// <summary>
    /// List of StudentCourses 
    /// </summary>
    public List<StudentCourse> StudentCourses { get; set; }
}