using KuSys.Entities.Requests;

namespace KuSys.Entities.TypeMappings;

/// <summary>
/// Type mappings for User entity
/// </summary>
public static class UserMappings
{
    /// <summary>
    /// Generates User entity from Register User request object.
    /// </summary>
    /// <param name="requestModel">Register User request object</param>
    /// <returns><see cref="User"/></returns>
    public static User ToEntity(this RegisterUserRequestModel requestModel)
    {
        User user = new User
        {
            FirstName = requestModel.FirstName,
            BirthDate = requestModel.BirthDate,
            UserName = requestModel.UserName,
            IsDeleted = false,
            LastName = requestModel.LastName,
            Email = requestModel.Email
        };
        return user;
    }
}