using KuSys.Contracts.RequestModels;
using KuSys.Contracts.ResponseModels;
using KuSys.Entities;

namespace KuSys.Services.Interfaces;

public interface IStudentService : IBaseService<Student>
{
    Task<NewStudentResponseModel> AddStudent(NewStudentRequestModel requestModel);
    Task<StudentListResponse> GetAll(GetStudentsRequest requestModel);
    Task<StudentResponseModel> GetStudentById(Guid id);
    Task<DeleteResponse<Guid>> DeleteStudentById(Guid id);
    Task<UpdateResponse<Guid>> UpdateStudent(Guid id,UpdateStudentModel requestModel);
    Task<StudentWithCoursesListResponse> GetStudentsWithCourses(GetStudentsWithCoursesRequest request);
    Task<StudentResponseModel> GetByUserId(Guid userId);
}