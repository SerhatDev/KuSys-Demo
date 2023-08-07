namespace KuSys.Contracts.ResponseModels;

public sealed class StudentWithCoursesListResponse
    : PagedResponse
{
    
    /// <summary>
    /// 
    /// </summary>
    public List<StudentsWithCoursesResponse> Data { get; set; }

    public StudentWithCoursesListResponse(List<StudentsWithCoursesResponse> data)
    {
        this.Data = data;
    }
}