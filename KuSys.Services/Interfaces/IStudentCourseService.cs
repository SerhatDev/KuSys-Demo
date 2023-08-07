using KuSys.Contracts.RequestModels;
using KuSys.Contracts.ResponseModels;
using KuSys.Entities;

namespace KuSys.Services.Interfaces;

public interface IStudentCourseService : IBaseService<StudentCourse>
{
    Task<CourseListResponseModel> GetAvailableCourses(Guid studentId,PagedRequest request);
    Task<CourseListResponseModel> GetJoinedCourses(Guid studentId,PagedRequest request);
    Task<SelectCourseResponseModel> SelectCourse(Guid studentId, SelectCourseRequest request);
}