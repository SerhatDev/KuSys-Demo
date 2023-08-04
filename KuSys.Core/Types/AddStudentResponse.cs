namespace KuSys.Core.Types;

/// <summary>
/// Response type for AddNewStudent operation.
/// </summary>
public sealed class AddStudentResponse
{
    /// <summary>
    /// Determine if the operation completed successfully
    /// </summary>
    public bool IsSuccess { get; set; }
    
    /// <summary>
    /// Id of the newly created user.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Error message if the operation was failed.
    /// </summary>
    public string ErrorMessage { get; set; }
}