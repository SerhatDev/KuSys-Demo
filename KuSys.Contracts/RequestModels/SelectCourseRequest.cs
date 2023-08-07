namespace KuSys.Contracts.RequestModels;

/// <summary>
/// Request model to select a course for a student
/// </summary>
public sealed class SelectCourseRequest
    : IBaseRequest
{
    /// <summary>
    /// Course Id.
    /// </summary>
    public Guid CourseId { get; set; }
}