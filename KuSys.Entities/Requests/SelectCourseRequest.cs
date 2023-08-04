namespace KuSys.Entities.Requests;

/// <summary>
/// Request model to select a course for a student
/// </summary>
public sealed class SelectCourseRequest
{
    /// <summary>
    /// Course Id.
    /// </summary>
    public Guid CourseId { get; set; }
}