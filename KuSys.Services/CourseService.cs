using KuSys.Contracts.RequestModels;
using KuSys.Contracts.ResponseModels;
using KuSys.Core;
using KuSys.Core.Types;
using KuSys.DataAccess.Repositories.Course;
using KuSys.Entities;
using KuSys.Services.Interfaces;
using Mapster;

namespace KuSys.Services;

/// <summary>
/// Service to manage Courses
/// </summary>
public sealed class CourseService
    : ICourseService
{
    /// <summary>
    /// Course repository for accessing the Database
    /// </summary>
    private readonly ICourseRepository _courseRepository;

    /// <summary>
    /// Constructor. Please use DependencyInjection.
    /// </summary>
    /// <param name="courseRepository">Course repository</param>
    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }
    
    
    /// <summary>
    /// Add new course.
    /// </summary>
    /// <param name="course">Course object to add into Database.</param>
    /// <returns>Returns <see cref="DbCreateResult{T}"/> which includes information about the operation. Use Success property to determine if the operation was successfully completed.</returns>
    public async Task<CourseResponseModel> AddNewCourse(NewCourseRequestModel course)
    {
        var result = await _courseRepository.AddNew(course.Adapt<Course>());
        if (result is null)
            return new CourseResponseModel()
            {
                IsSuccess = false
            };
        var responseModel= result.Data.Adapt<CourseResponseModel>();
        responseModel.IsSuccess = result.Result == OperationResult.Success;
        return responseModel;
    }

    /// <summary>
    /// Gett all courses
    /// </summary>
    /// <returns><see cref="DbQueryListResult{T}"/></returns>
    public async Task<CourseListResponseModel> GetAll(GetCoursesRequestModel request)
    {
        var result = await _courseRepository.GetAll(request);
        var returnValue = result.Adapt<PagedResponse<CourseResponseModel>>();
        return new CourseListResponseModel(returnValue);
    }
}