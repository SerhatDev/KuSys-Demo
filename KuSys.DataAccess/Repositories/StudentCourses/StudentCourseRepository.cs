using KuSys.DataAccess.Repositories.Student;
using KuSys.Entities;

namespace KuSys.DataAccess.Repositories.StudentCourses;

/// <summary>
/// Student Course repository to manage database operations.
/// </summary>
public sealed class StudentCourseRepository
    : BaseRepository<StudentCourse, Guid>, IStudentCourseRepository
{
    /// <inheritdoc />
    public StudentCourseRepository(KuSysDbContext dbContext) 
        : base(dbContext)
    {
    }
}