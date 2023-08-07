namespace KuSys.Contracts.RequestModels;

/// <summary>
/// Request model to Add new course.
/// </summary>
public sealed class NewCourseRequestModel 
    : IBaseRequest
{
    /// <summary>
    /// Course name
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Course code
    /// </summary>
    public string Code { get; set; }
}