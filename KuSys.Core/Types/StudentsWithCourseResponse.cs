namespace KuSys.Core.Types;

/// <summary>
/// Response type for Request of students with course information.
/// </summary>
public sealed class StudentsWithCourseResponse
{
    /// <summary>
    /// Student information.
    /// </summary>
    public GetStudentResponse Student { get; set; }
    
    /// <summary>
    /// List of the students matched courses.
    /// </summary>
    public List<CoursesResponse> Courses { get; set; }
}