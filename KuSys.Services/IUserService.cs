using KuSys.Core.Types;
using KuSys.Entities;
using KuSys.Entities.Requests;

namespace KuSys.Services;

public interface IUserService : IBaseService<User>
{
    Task<DbCreateResult<Guid>> CreateUser(RegisterUserRequestModel model);
    Task<LoginResponse> Login(LoginRequestModel requestModel);
    Task<bool> MakeAdmin(LoginRequestModel requestModel);
}