using KuSys.Contracts.RequestModels;
using KuSys.Contracts.ResponseModels;
using KuSys.Core.Types;

namespace KuSys.DataAccess.Repositories.Student;

/// <summary>
/// Repository class for managing database operations of Students.
/// </summary>
public interface IStudentRepository
    : IBaseRepository<Entities.Student, Guid>
{
    /// <summary>
    /// Get students with their joined courses information.
    /// </summary>
    /// <param name="request">Paging information of the request.</param>
    /// <returns>List of students with their joined courses as <see cref="StudentsWithCoursesResponse"/> model</returns>
    Task<PagedResponse<StudentsWithCoursesResponse>> StudentsWithCourses(GetStudentsWithCoursesRequest request);
}