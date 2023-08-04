using KuSys.Core.Types;
using KuSys.DataAccess.Repositories.Course;
using KuSys.DataAccess.Repositories.Student;
using KuSys.DataAccess.Repositories.StudentCourses;
using KuSys.Entities;
using KuSys.Entities.Requests;
using KuSys.Entities.TypeMappings;

namespace KuSys.Services;

/// <summary>
/// StudentCourse service type.
/// </summary>
public sealed class StudentCourseService : IStudentCourseService
{
    private readonly IStudentCourseRepository _studentCourseRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IStudentRepository _studentRepository;

    /// <summary>
    /// Constructor. Please use DependencyInjection.
    /// </summary>
    /// <param name="studentCourseRepository">StudentCourse repostiory</param>
    /// <param name="courseRepository">course repository</param>
    /// <param name="studentRepository">Student Repository</param>
    public StudentCourseService(IStudentCourseRepository studentCourseRepository,ICourseRepository courseRepository,IStudentRepository studentRepository)
    {
        _studentCourseRepository = studentCourseRepository;
        _courseRepository = courseRepository;
        _studentRepository = studentRepository;
    }
    
    /// <summary>
    /// Get avaliable course list for specified user.
    /// </summary>
    /// <param name="studentId">Id of the user</param>
    /// <returns><see cref="List{T}"/></returns>
    public async Task<List<CoursesResponse>> GetAvailableCourses(Guid studentId)
    {
        // Get the courses in which user has not been matched yet.
        var avaliableCourses =
            await _courseRepository.Query(course => course.StudentCourses.All(sc => sc.StudentId != studentId));
        
        // Map entity to response object type.
        var data = avaliableCourses.Data?
            .Select(x => x.ToResponse())
            .ToList();
        
        // return data
        return data;

    }

    /// <summary>
    /// Get the list of matched courses for specified student.
    /// </summary>
    /// <param name="studentId">Id of the student</param>
    /// <returns><see cref="List{T}"/></returns>
    public async Task<List<CoursesResponse>> GetJoinedCourses(Guid studentId)
    {
        // get the courses in which user has been matched.
        var avaliableCourses =
            await _courseRepository.Query(course => course.StudentCourses.Any(sc => sc.StudentId == studentId));
        
        // Map entity to response object
        var data = avaliableCourses.Data?
            .Select(x => x.ToResponse())
            .ToList();
        
        // return data
        return data;
    }

    /// <summary>
    /// Add new course for specified student
    /// </summary>
    /// <param name="studentId">Student Id</param>
    /// <param name="request">Course Request</param>
    /// <returns><see cref="DbCreateResult{T}"/></returns>
    /// <exception cref="Exception"></exception>
    public async Task<DbCreateResult<StudentCourse>> SelectCourse(Guid studentId, SelectCourseRequest request)
    {
        // Get students joined courses and check if user was already joined to specified course before
        var availableCourses = await this.GetJoinedCourses(studentId);
        bool isCourseSelected = availableCourses.Any(x => x.Id == request.CourseId);
        
        // If stundet was already joined to specified course, throw an error.
        if (isCourseSelected)
            throw new Exception("Course can not be selected more than once!");
        
        // Create new StudentCourse object with specified values
        StudentCourse studentCourse = new StudentCourse()
        {
            StudentId = studentId,
            CourseId = request.CourseId
        };
        
        // Save StundetCourse object into database.
        var result = await _studentCourseRepository.AddNew(studentCourse);
        
        // return the result.
        return result;
    }
}