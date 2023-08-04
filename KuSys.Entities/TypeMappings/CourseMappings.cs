using KuSys.Core.Types;
using KuSys.Entities.Requests;

namespace KuSys.Entities.TypeMappings;

/// <summary>
/// Course type mappings extensions.
/// </summary>
public static class CourseMappings
{
    /// <summary>
    /// Gets course entity object from Provided data.
    /// </summary>
    /// <param name="requestModel">Request data</param>
    /// <returns><see cref="Course"/></returns>
    public static Course ToEntity(this AddNewCourseRequestModel requestModel)
    {
        return new Course()
        {
            Name = requestModel.Name,
            Code = requestModel.Code
        };
    }

    /// <summary>
    /// Generates CourseResponse object from the Course entity
    /// </summary>
    /// <param name="course">Entity object</param>
    /// <returns><see cref="CoursesResponse"/></returns>
    public static CoursesResponse ToResponse(this Course course)
    {
        return new CoursesResponse()
        {
            Code = course.Code,
            Name = course.Name,
            Id = course.Id
        };
    }
}