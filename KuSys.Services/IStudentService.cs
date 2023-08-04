using KuSys.Core.Types;
using KuSys.Entities;
using KuSys.Entities.Requests;

namespace KuSys.Services;

public interface IStudentService : IBaseService<Student>
{
    Task<AddStudentResponse> AddStudent(AddStudentRequestModel requestModel);
    Task<PagedResponse<Student>> GetAll(GetAllStudentRequest requestModel);
    Task<GetStudentResponse> GetStudentById(Guid id);
    Task<DbDeleteResult<Guid>> DeleteStudentById(Guid id);
    Task<DbUpdateResult<Student>> UpdateStudent(Guid id,UpdateStudentModel requestModel);
    Task<DbQueryListResult<StudentsWithCourseResponse>> GetStudentsWithCourses();
}