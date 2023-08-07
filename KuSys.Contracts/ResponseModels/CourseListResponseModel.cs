using KuSys.Core.Types;
using Mapster;

namespace KuSys.Contracts.ResponseModels;

/// <summary>
/// Response model for list of courses
/// </summary>
public sealed class CourseListResponseModel
    : PagedResponse
{
    /// <summary>
    /// Initialize response with data.
    /// </summary>
    /// <param name="courses"></param>
    public CourseListResponseModel(PagedResponse<CourseResponseModel> courses)
    {
        // Map course entity to CourseResponseModel
        this.Courses = courses.Data.Select(course 
            => course.Adapt<CourseResponseModel>())
            .ToList();
        
        // Set paging response values.
        this.PageNumber = courses.PageNumber;
        this.PageSize = courses.PageSize;
        this.PageCount = courses.PageCount;
        this.RecordsCount = courses.RecordsCount;
        this.IsSuccess = true;
    }
    
    /// <summary>
    /// List of courses
    /// </summary>
    public List<CourseResponseModel> Courses { get; set; }
}