namespace KuSys.Core.Types;

/// <summary>
/// Response type for Get Single Student Requests.
/// </summary>
public sealed class GetStudentResponse
{
    /// <summary>
    /// First name of the student.
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Last name of the student.
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// Birth date of the student.
    /// </summary>
    public DateTime BirthDate { get; set; }
    
    /// <summary>
    /// Gender of the student. (0=Wont Provide, 1=Male, 2=Female)
    /// </summary>
    public Gender Gender { get; set; }
    
    /// <summary>
    /// Id of the student.
    /// </summary>
    public Guid Id { get; set; }
}