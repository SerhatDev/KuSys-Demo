using KuSys.Core;

namespace KuSys.Contracts.ResponseModels;

/// <summary>
/// Response Type for student.
/// </summary>
public sealed class StudentResponseModel
    : BaseResponse
{
    /// <summary>
    /// Id of the student.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// FirstName of the student.
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
    /// Gender of the student
    /// </summary>
    public Gender Gender { get; set; }
}