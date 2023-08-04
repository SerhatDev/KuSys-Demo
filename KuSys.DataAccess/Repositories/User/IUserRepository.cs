using System.Linq.Expressions;
using KuSys.Core.Types;

namespace KuSys.DataAccess.Repositories.User;

public interface IUserRepository
{
    Task<DbCreateResult<Guid>> AddUser(Entities.User model,string password);
    Task<DbCreateResult<Entities.User>> AddStudentUser(Entities.User model, string password);
    Task<DbUpdateResult<Entities.User>> UpdateUser(Entities.User model);
    Task<DbDeleteResult<Guid>> DeleteUser(Entities.User model);
    Task<DbDeleteResult<Guid>> DeleteUserById(Guid id);
    Task<DbQuerySingleResult<Entities.User>> GetUserById(Guid id);
    Task<DbQueryListResult<Entities.User>> GetUsers();
    Task<DbQueryListResult<Entities.User>> Query(Expression<Func<Entities.User, bool>> query);
}