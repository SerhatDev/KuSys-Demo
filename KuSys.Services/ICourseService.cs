using KuSys.Core.Types;
using KuSys.Entities;
using KuSys.Entities.Requests;

namespace KuSys.Services;

public interface ICourseService : IBaseService<Course>
{
    Task<DbCreateResult<Course>> AddNewCourse(AddNewCourseRequestModel course);
    Task<DbQueryListResult<Course>> GetAll();
}