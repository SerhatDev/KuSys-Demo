namespace KuSys.Contracts.ResponseModels;

public sealed class StudentsWithCoursesResponse
{
    public StudentResponseModel Student { get; set; }
    public List<CourseResponseModel> Courses { get; set; }
}