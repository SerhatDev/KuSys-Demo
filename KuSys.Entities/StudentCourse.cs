namespace KuSys.Entities;

/// <summary>
/// StudentCourses entity
/// </summary>
public sealed class StudentCourse  : EntityBase<Guid>
{
    /// <summary>
    /// Id of the student
    /// </summary>
    public Guid StudentId { get; set; }
    
    
    /// <summary>
    /// Id of the course
    /// </summary>
    public Guid CourseId { get; set; }
    
    
    
    /// <summary>
    /// Student entity 
    /// </summary>
    public Student Student { get; set; }
    
    
    /// <summary>
    /// Course Entity
    /// </summary>
    public Course Course { get; set; }
}