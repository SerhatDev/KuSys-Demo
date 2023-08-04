using KuSys.Core;

namespace KuSys.Entities;

/// <summary>
/// Student Entity
/// </summary>
public sealed class Student : EntityBase<Guid>
{
    /// <summary>
    /// First name of student
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Last name of student
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// Birth date  of student
    /// </summary>
    public DateTime BirthDate { get; set; }
    
    /// <summary>
    /// Gender  of student
    /// </summary>
    public Gender Gender { get; set; }
    
    /// <summary>
    /// User Id  of student
    /// </summary>
    public Guid UserId { get; set; }
    
    
    /// <summary>
    /// User object of the stundets User
    /// </summary>
    public User User { get; set; }
    
    
    /// <summary>
    /// List of StudentCourses
    /// </summary>
    public List<StudentCourse> StudentCourses { get; set; }
}