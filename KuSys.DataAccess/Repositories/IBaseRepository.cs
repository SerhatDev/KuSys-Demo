using System.Linq.Expressions;
using KuSys.Contracts.RequestModels;
using KuSys.Core.Types;

namespace KuSys.DataAccess.Repositories;

/// <summary>
/// Base type for Repositories to manage Database operations.
/// </summary>
/// <typeparam name="TModel">Entity type for databaes operations.</typeparam>
/// <typeparam name="TKey">The key type of the provided entity</typeparam>
public interface IBaseRepository<TModel, TKey>
{
    /// <summary>
    /// Add new data into database.
    /// </summary>
    /// <param name="model">Data to save</param>
    /// <returns></returns>
    Task<DbCreateResult<TModel>> AddNew(TModel model);
    
    /// <summary>
    /// Get all data for the specified entity.
    /// </summary>
    /// <returns>List of the data for specified entity as <see cref="DbQueryListResult{T}"/></returns>
    Task<DbQueryListResult<TModel>> GetAll();
    
    /// <summary>
    /// Get all data for the specified entity with pagination.
    /// </summary>
    /// <param name="pagedRequest">Request information which includes paging information. (Should be derived from <see cref="PagedRequest"/></param>
    /// <typeparam name="TPagedRequest">Type of the request (Should be derived from <see cref="PagedRequest"/></typeparam>
    /// <returns></returns>
    Task<PagedResponse<TModel>> GetAll<TPagedRequest>(TPagedRequest pagedRequest) where TPagedRequest : PagedRequest;

    /// <summary>
    /// Get single item for specified Id.
    /// </summary>
    /// <param name="id">Id of the data.</param>
    /// <returns>Returns <see cref="DbQuerySingleResult{T}"/></returns>
    Task<DbQuerySingleResult<TModel>> GetById(TKey id);
    
    /// <summary>
    /// Update entity with given information.
    /// </summary>
    /// <param name="model">Model to update</param>
    /// <returns>Returns <see cref="DbUpdateResult{T}"/></returns>
    Task<DbUpdateResult<TModel>> Update(TModel model);
    
    /// <summary>
    /// Delete the entity by model.
    /// </summary>
    /// <param name="model">Model to delete.</param>
    /// <returns>Returns <see cref="DbDeleteResult{T}"/></returns>
    Task<DbDeleteResult<TKey>> Delete(TModel model);
    
    /// <summary>
    /// Delete the entity by it's id.
    /// </summary>
    /// <param name="id">Id of the entity to delete.</param>
    /// <returns></returns>
    Task<DbDeleteResult<TKey>> DeleteById(TKey id);

    /// <summary>
    /// Custom queries to get data.
    /// </summary>
    /// <param name="query">Where clause</param>
    /// <returns>Returns <see cref="DbQueryListResult{T}"/></returns>
    Task<DbQueryListResult<TModel>> Query(Expression<Func<TModel, bool>> query);

    /// <summary>
    /// Custom queries to get data with paging applied.
    /// </summary>
    /// <param name="query">Where clause</param>
    /// <param name="pagedRequest">Paging information.</param>
    /// <returns>Returns <see cref="PagedResponse{T}"/></returns>
    Task<PagedResponse<TModel>> Query<TPagedRequest>(Expression<Func<TModel, bool>> query, TPagedRequest pagedRequest) where TPagedRequest : PagedRequest;
}