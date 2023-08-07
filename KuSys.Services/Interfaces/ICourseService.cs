using KuSys.Contracts.RequestModels;
using KuSys.Contracts.ResponseModels;
using KuSys.Entities;

namespace KuSys.Services.Interfaces;

public interface ICourseService : IBaseService<Course>
{
    Task<CourseResponseModel> AddNewCourse(NewCourseRequestModel course);
    Task<CourseListResponseModel> GetAll(GetCoursesRequestModel request);
}