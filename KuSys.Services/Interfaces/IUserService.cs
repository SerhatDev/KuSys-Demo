using KuSys.Contracts.RequestModels;
using KuSys.Contracts.ResponseModels;
using KuSys.Entities;

namespace KuSys.Services.Interfaces;

public interface IUserService : IBaseService<User>
{
    Task<RegisterResponseModel> CreateUser(RegisterRequestModel model);
    Task<LoginResponseModel> Login(LoginRequestModel requestModel);
    Task<bool> MakeAdmin(LoginRequestModel requestModel);
}