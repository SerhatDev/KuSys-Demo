using KuSys.Core.Types;
using KuSys.Entities.Requests;

namespace KuSys.Entities.TypeMappings;

/// <summary>
/// Student type mappings extensions.
/// </summary>
public static class StudentMappings
{
    /// <summary>
    /// Gets student entity object from Provided data.
    /// </summary>
    /// <param name="requestModel">Request data</param>
    /// <returns><see cref="Student"/></returns>
    public static Student ToEntity(this AddStudentRequestModel requestModel)
    {
        Student student = new Student()
        {
            BirthDate = requestModel.BirthDate,
            FirstName = requestModel.FirstName,
            Gender = requestModel.Gender,
            IsDeleted = false,
            LastName = requestModel.LastName
        };
        return student;
    }

    /// <summary>
    /// Gets student entity object from Provided data.
    /// </summary>
    /// <param name="requestModel">Request data</param>
    /// <returns><see cref="Student"/></returns>
    public static Student ToEntity(this UpdateStudentModel requestModel)
    {
        return new Student()
        {
            BirthDate = requestModel.BirthDate,
            FirstName = requestModel.FirstName,
            LastName = requestModel.LastName,
            Gender = requestModel.Gender
        };
    }

    /// <summary>
    /// Generates StudentResponse from student entity.
    /// </summary>
    /// <param name="student">Entity object</param>
    /// <returns><see cref="GetStudentResponse"/></returns>
    public static GetStudentResponse ToResponse(this Student student)
    {
        GetStudentResponse model = new GetStudentResponse()
        {
            BirthDate = student.BirthDate,
            FirstName = student.FirstName,
            Gender = student.Gender,
            LastName = student.LastName,
            Id = student.Id
        };
        return model;
    }

    /// <summary>
    /// Generates new user entity from StudentRequest model.
    /// </summary>
    /// <param name="student">Student entity object</param>
    /// <returns><see cref="User"/></returns>
    public static User ToUserEntity(this AddStudentRequestModel student)
    {
        User user = new User()
        {
            FirstName = student.FirstName,
            LastName = student.LastName,
            BirthDate = student.BirthDate,
            IsDeleted = false,
            Email = student.Email,
            UserName = student.UserName
        };
        return user;
    }
}