using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using KuSys.Core.Constants;
using KuSys.Core.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KuSys.Core.Exceptions;

namespace KuSys.DataAccess.Repositories.User;

/// <summary>
/// User repository to manage database operations.
/// </summary>
// We dont implement base repository because we want to use AspNet.Identity to control users.
public sealed class UserRepository : IUserRepository
{
    private readonly UserManager<Entities.User> _userManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userManager">User manager</param>
    public UserRepository(UserManager<Entities.User> userManager)
    {
        _userManager = userManager;
    }
    
    /// <summary>
    /// Add new user with specified password. No role would've been assigned!
    /// </summary>
    /// <param name="model">User model to create.</param>
    /// <param name="password">Users password</param>
    /// <returns>Returns <see cref="DbCreateResult{T}">Id</see> of newly created user.</returns>
    public async Task<DbCreateResult<Guid>> AddUser(Entities.User model,string password)
    {
        // Create user with provied information via userManager.
        var createUserResult = await _userManager.CreateAsync(model);
        
        // If create result was not successful, return failed result
        if (!createUserResult.Succeeded)
            throw new InvalidException(createUserResult.Errors);
        
        // Since user has been created successfully, we need to assing the password. 
        var passwordResult = await _userManager.AddPasswordAsync(model, password);
        
        // If we couldn't assign password, hard delete the user and return failed response.
        if(!passwordResult.Succeeded)
        {
            // Hard delete the user.
            await _userManager.DeleteAsync(model);
            
            throw new InvalidException(passwordResult.Errors);
        }

        // If code reaches to that point, we return Success Response with newly created userId.
        return DbResultBuilder.CreateResultSuccess(model.Id);
    }

    /// <summary>
    /// Add new user for Student. Created user will be assigned into Student Role.
    /// </summary>
    /// <param name="model">User model to create.</param>
    /// <param name="password">Users password</param>
    /// <returns>Returns <see cref="DbCreateResult{T}">Id</see> of newly created user.</returns>
    public async Task<DbCreateResult<Entities.User>> AddStudentUser(Entities.User model, string password)
    {
        // Create user with provied information via userManager.
        var createUserResult = await _userManager.CreateAsync(model);
        
        // If create result was not successful, return failed result
        if (!createUserResult.Succeeded)
            throw new InvalidException(createUserResult.Errors);
        
        // Since user has been created successfully, we need to assign the password. 
        var passwordResult = await _userManager.AddPasswordAsync(model, password);
        
        // If we couldn't assign password, hard delete the user and return failed response.
        if(!passwordResult.Succeeded)
        {
            // Hard delete user
            await _userManager.DeleteAsync(model);
             
            throw new InvalidException(passwordResult.Errors);
        }

        // Add user into Student Role
        await _userManager.AddToRoleAsync(model, DefaultRoles.Student);
        
        // If code reaches to that point, we return Success Response with newly created userId.
        return DbResultBuilder.CreateResultSuccess(model);
    }

    /// <summary>
    /// Update the user with given data.
    /// </summary>
    /// <param name="model">Data to update</param>
    /// <returns></returns>
    public async Task<DbUpdateResult<Entities.User>> UpdateUser(Entities.User model)
    {
        // Update user via userManager
        var updateResult = await _userManager.UpdateAsync(model);
        
        // if update operation was successful return Success response with update model.
        if (updateResult.Succeeded)
            return DbResultBuilder.UpdateResultSuccess(model);
         
        throw new InvalidException(updateResult.Errors);
    }

    /// <summary>
    /// Delete user with given information. (SOFT-DELETE)
    /// </summary>
    /// <param name="model">User to delete</param>
    /// <returns>Returns <see cref="DbDeleteResult{T}"/></returns>
    public async Task<DbDeleteResult<Guid>> DeleteUser(Entities.User model)
    {
        // Set isDeleted prop to true as we want to soft-delete
        model.IsDeleted = true;
        
        // Update the user
        var updateResult = await _userManager.UpdateAsync(model);
        
        // if update operation was successfull return Success response with update model.
        if (updateResult.Succeeded)
            return DbResultBuilder.DeleteResultSuccess(model.Id);
        
        throw new InvalidException(updateResult.Errors);
    }

    /// <summary>
    /// Delete user by userId.
    /// </summary>
    /// <param name="id">Id of the user.</param>
    /// <returns>Returns <see cref="DbDeleteResult{T}"/></returns>
    public async Task<DbDeleteResult<Guid>> DeleteUserById(Guid id)
    {
        // Get the user with given Id
        var foundUser = await _userManager.FindByIdAsync(id.ToString());

        if (foundUser is null)
            throw new DataNotFoundException($"User with given id ({id.ToString()}) was not found!");
        
        // set isDeleted prop to true since we want to soft-delete
        foundUser.IsDeleted = true;
        
        // Update the user
        var updateResult = await _userManager.UpdateAsync(foundUser);
        
        // if update operation was successfull return Success response with update model.
        if (updateResult.Succeeded)
            return DbResultBuilder.DeleteResultSuccess(foundUser.Id);

        throw new InvalidException(updateResult.Errors);
    }

    /// <summary>
    /// Get user by id.
    /// </summary>
    /// <param name="id">Id of the user.</param>
    /// <returns>Returns <see cref="DbQuerySingleResult{T}"/></returns>
    public async Task<DbQuerySingleResult<Entities.User>> GetUserById(Guid id)
    {
        // Get the user with given Id
        var foundUser = await _userManager.FindByIdAsync(id.ToString());
        
        if (foundUser is null)
            throw new DataNotFoundException($"User with given id ({id.ToString()}) was not found!");
        
        // Return found user data with Success Response
        return DbResultBuilder.SingleResultSuccess(foundUser);
    }

    /// <summary>
    /// Get all users.
    /// </summary>
    /// <returns><see cref="DbQueryListResult{T}"/></returns>
    public async Task<DbQueryListResult<Entities.User>> GetUsers()
    {
        // Get the list of users.
        var list = await _userManager.Users.ToListAsync();
        
        // Return the list as Success Response
        return DbResultBuilder.ListResultSuccess(list);
    }

    /// <summary>
    /// Get users with specified query.
    /// </summary>
    /// <param name="query">Lambda query to filter users.</param>
    /// <returns></returns>
    public async Task<DbQueryListResult<Entities.User>> Query(Expression<Func<Entities.User, bool>> query)
    {
        // Apply specfied filter
        var list = await _userManager.Users
            .Where(query)
            .ToListAsync();
        
        // Return the results as Success Response.
        return DbResultBuilder.ListResultSuccess(list);
    }
}