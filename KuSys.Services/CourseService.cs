using KuSys.Core.Types;
using KuSys.DataAccess.Repositories.Course;
using KuSys.Entities;
using KuSys.Entities.Requests;
using KuSys.Entities.TypeMappings;

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
    public async Task<DbCreateResult<Course>> AddNewCourse(AddNewCourseRequestModel course)
    {
        var result = await _courseRepository.AddNew(course.ToEntity());
        return result;
    }

    /// <summary>
    /// Gett all courses
    /// </summary>
    /// <returns><see cref="DbQueryListResult{T}"/></returns>
    public async Task<DbQueryListResult<Course>> GetAll()
    {
        var result = await _courseRepository.GetAll();
        return result;
    }
}