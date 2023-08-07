namespace KuSys.Contracts.ResponseModels;

/// <summary>
/// Response model for single course.
/// </summary>
public sealed class CourseResponseModel
    : BaseResponse
{
    /// <summary>
    /// Name of the course
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Code of the course
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// Id of the course.
    /// </summary>
    public Guid Id { get; set; }
}