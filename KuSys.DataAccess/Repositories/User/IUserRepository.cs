using System.Linq.Expressions;
using KuSys.Core.Types;

namespace KuSys.DataAccess.Repositories.User;

/// <summary>
/// Repository class for managing database operations of Users.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Add new user with no role.
    /// </summary>
    /// <param name="model">User entity</param>
    /// <param name="password">Password of the user</param>
    /// <returns><see cref="DbCreateResult{T}"/> with newly added User's id</returns>
    Task<DbCreateResult<Guid>> AddUser(Entities.User model,string password);
    
    /// <summary>
    /// Adds user for a student, Student role will be assigned.
    /// </summary>
    /// <param name="model">User Entity</param>
    /// <param name="password">Password of the user</param>
    /// <returns><see cref="DbCreateResult{T}"/> with newly added user model.</returns>
    Task<DbCreateResult<Entities.User>> AddStudentUser(Entities.User model, string password);
    
    /// <summary>
    /// Updates user entity with given values.
    /// </summary>
    /// <param name="model">User entity update</param>
    /// <returns><see cref="DbUpdateResult{T}"/> with updated values.</returns>
    Task<DbUpdateResult<Entities.User>> UpdateUser(Entities.User model);
    
    /// <summary>
    /// Soft deletes user by user entity.
    /// </summary>
    /// <param name="model">User entity to soft delete.</param>
    /// <returns><see cref="DbDeleteResult{T}"/> with deleted user's id</returns>
    Task<DbDeleteResult<Guid>> DeleteUser(Entities.User model);
    
    /// <summary>
    /// Soft deletes user by user id.
    /// </summary>
    /// <param name="id">User id to soft delete.</param>
    /// <returns><see cref="DbDeleteResult{T}"/> with deleted user's id</returns>
    Task<DbDeleteResult<Guid>> DeleteUserById(Guid id);
    
    
    /// <summary>
    /// Get user entity by it's id.
    /// </summary>
    /// <param name="id">User id to get details of.</param>
    /// <returns><see cref="DbQuerySingleResult{T}"/> with requested user entity object.</returns>
    Task<DbQuerySingleResult<Entities.User>> GetUserById(Guid id);
    
    /// <summary>
    /// Get all users in the system.
    /// </summary>
    /// <returns><see cref="DbQueryListResult{T}"/></returns>
    Task<DbQueryListResult<Entities.User>> GetUsers();
    
    /// <summary>
    /// Custom query user table.
    /// </summary>
    /// <param name="query">Where condition.</param>
    /// <returns><see cref="DbQueryListResult{T}"/></returns>
    Task<DbQueryListResult<Entities.User>> Query(Expression<Func<Entities.User, bool>> query);
}