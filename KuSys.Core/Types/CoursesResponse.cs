namespace KuSys.Core.Types;

/// <summary>
/// Response type for CourseOperations.
/// </summary>
public sealed class CoursesResponse
{
    /// <summary>
    /// Name of the course.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Code of the course.
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// Id of the course.
    /// </summary>
    public Guid Id { get; set; }
}