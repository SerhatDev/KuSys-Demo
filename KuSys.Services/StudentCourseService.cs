using KuSys.Contracts.RequestModels;
using KuSys.Contracts.ResponseModels;
using KuSys.Core.Types;
using KuSys.DataAccess.Repositories.Course;
using KuSys.DataAccess.Repositories.Student;
using KuSys.DataAccess.Repositories.StudentCourses;
using KuSys.Entities;
using KuSys.Services.Interfaces;
using Mapster;

namespace KuSys.Services;

/// <summary>
/// StudentCourse service type.
/// </summary>
public sealed class StudentCourseService : IStudentCourseService
{
    private readonly IStudentCourseRepository _studentCourseRepository;
    private readonly ICourseRepository _courseRepository;

    /// <summary>
    /// Constructor. Please use DependencyInjection.
    /// </summary>
    /// <param name="studentCourseRepository">StudentCourse repostiory</param>
    /// <param name="courseRepository">course repository</param>
    /// <param name="studentRepository">Student Repository</param>
    public StudentCourseService(IStudentCourseRepository studentCourseRepository, ICourseRepository courseRepository)
    {
        _studentCourseRepository = studentCourseRepository;
        _courseRepository = courseRepository;
    }

    /// <summary>
    /// Get avaliable course list for specified user.
    /// </summary>
    /// <param name="studentId">Id of the user</param>
    /// <param name="request"></param>
    /// <returns><see cref="List{T}"/></returns>
    public async Task<CourseListResponseModel> GetAvailableCourses(Guid studentId, PagedRequest request)
    {
        // Get the courses in which user has not been matched yet.
        var avaliableCourses =
            await _courseRepository.Query(course => course.StudentCourses.All(sc => sc.StudentId != studentId),
                request);

        var returnModel = avaliableCourses.Adapt<PagedResponse<CourseResponseModel>>();


        // return data
        return new CourseListResponseModel(returnModel);
    }

    /// <summary>
    /// Get the list of matched courses for specified student.
    /// </summary>
    /// <param name="studentId">Id of the user</param>
    /// <returns><see cref="List{T}"/></returns>
    public async Task<CourseListResponseModel> GetJoinedCourses(Guid studentId, PagedRequest request)
    {
        // Get the courses in which user has not been matched yet.
        var avaliableCourses =
            await _courseRepository.Query(course => course.StudentCourses.Any(sc => sc.StudentId == studentId),
                request);

        var returnModel = avaliableCourses.Adapt<PagedResponse<CourseResponseModel>>();
        // return data
        return new CourseListResponseModel(returnModel);
    }

    /// <summary>
    /// Add new course for specified student
    /// </summary>
    /// <param name="studentId">Student Id</param>
    /// <param name="request">Course Request</param>
    /// <returns><see cref="DbCreateResult{T}"/></returns>
    /// <exception cref="Exception"></exception>
    public async Task<SelectCourseResponseModel> SelectCourse(Guid studentId, SelectCourseRequest request)
    {
        // Get students joined courses and check if user was already joined to specified course before
        var availableCourses =
            await _courseRepository.Query(course => course.StudentCourses.Any(sc => sc.StudentId == studentId));
        bool isCourseSelected = availableCourses.Data.Any(x => x.Id == request.CourseId);

        // If stundet was already joined to specified course, throw an error.
        if (isCourseSelected)
            throw new Exception("Course can not be selected more than once!");

        // WithData new StudentCourse object with specified values
        StudentCourse studentCourse = new StudentCourse()
        {
            StudentId = studentId,
            CourseId = request.CourseId
        };

        // Save StundetCourse object into database.
        var result = await _studentCourseRepository.AddNew(studentCourse);

        // return the result.
        return new SelectCourseResponseModel(result.Id);
    }
}