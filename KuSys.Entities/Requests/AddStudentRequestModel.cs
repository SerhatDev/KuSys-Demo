using KuSys.Core;

namespace KuSys.Entities.Requests;

/// <summary>
/// Request model to Add new Student
/// </summary>
public class AddStudentRequestModel
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
    /// Birth date of student
    /// </summary>
    public DateTime BirthDate { get; set; }
    
    /// <summary>
    /// Gender of student
    /// </summary>
    public Gender Gender { get; set; }
    
    /// <summary>
    /// Email of student (should be unique and will be used to login into system)
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Password for student's user
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// User name (should be unique)
    /// </summary>
    public string UserName { get; set; }
}