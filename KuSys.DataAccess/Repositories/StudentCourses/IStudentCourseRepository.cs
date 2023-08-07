using KuSys.Entities;

namespace KuSys.DataAccess.Repositories.StudentCourses;

/// <summary>
/// Repository class for managing database operations of StudentCourse.
/// </summary>
public interface IStudentCourseRepository
    : IBaseRepository<StudentCourse, Guid>
{
    
}