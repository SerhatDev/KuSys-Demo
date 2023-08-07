using KuSys.Core.Types;
using Mapster;

namespace KuSys.Contracts.ResponseModels;

/// <summary>
/// Response type for List of students
/// </summary>
public sealed class StudentListResponse
    : PagedResponse
{
    /// <summary>
    /// Students.
    /// </summary>
    public List<StudentResponseModel> Students { get; set; }
    
    /// <summary>
    /// Initialize response with data.
    /// </summary>
    /// <param name="students"></param>
    public StudentListResponse(PagedResponse<StudentResponseModel> students)
    {
        // Map course entity to CourseResponseModel
        this.Students = students.Data;
        
        // Set paging response values.
        this.PageNumber = students.PageNumber;
        this.PageSize = students.PageSize;
        this.PageCount = students.PageCount;
        this.RecordsCount = students.RecordsCount;
        this.IsSuccess = true;
    }

    public StudentListResponse()
    {
        
    }
}