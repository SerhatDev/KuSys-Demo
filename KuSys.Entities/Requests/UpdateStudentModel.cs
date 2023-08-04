using KuSys.Core;

namespace KuSys.Entities.Requests;

/// <summary>
/// Request model for Update Student operation.
/// </summary>
public sealed class UpdateStudentModel
{
    /// <summary>
    /// New value of First name
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// New value of Last name
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// New value of Birth date
    /// </summary>
    public DateTime BirthDate { get; set; }
    
    
    /// <summary>
    /// New  value of gender.
    /// </summary>
    public Gender Gender { get; set; }
}