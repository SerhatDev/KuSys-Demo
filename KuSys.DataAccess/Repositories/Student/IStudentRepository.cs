using KuSys.Core.Types;

namespace KuSys.DataAccess.Repositories.Student;

public interface IStudentRepository
    : IBaseRepository<Entities.Student, Guid>
{
    Task<DbQueryListResult<StudentsWithCourseResponse>> StudentsWithCourses();
}