namespace KuSys.Entities.Requests;

/// <summary>
/// Request model to Add new course.
/// </summary>
public sealed class AddNewCourseRequestModel : IBaseRequest
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