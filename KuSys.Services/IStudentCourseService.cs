using KuSys.Core.Types;
using KuSys.Entities;
using KuSys.Entities.Requests;

namespace KuSys.Services;

public interface IStudentCourseService : IBaseService<StudentCourse>
{
    Task<List<CoursesResponse>> GetAvailableCourses(Guid studentId);
    Task<List<CoursesResponse>> GetJoinedCourses(Guid studentId);
    Task<DbCreateResult<StudentCourse>> SelectCourse(Guid studentId, SelectCourseRequest request);
}