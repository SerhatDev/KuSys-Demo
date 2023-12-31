using System.Linq.Expressions;
using KuSys.Contracts.RequestModels;
using KuSys.Core.Exceptions;
using KuSys.Core.Helpers;
using KuSys.Core.Types;
using KuSys.Entities;
using Microsoft.EntityFrameworkCore;

namespace KuSys.DataAccess.Repositories;

/// <inheritdoc />
public abstract class BaseRepository<TModel,TKey> 
    : IBaseRepository<TModel,TKey>
where TModel : EntityBase<TKey>
{
    private readonly KuSysDbContext _dbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="dbContext"></param>
    protected BaseRepository(KuSysDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <inheritdoc />
    public async Task<DbCreateResult<TModel>> AddNew(TModel model)
    {
        // Get databaseSet for current entity.
        var dbSet = _dbContext.Set<TModel>();
        
        // Add provided model into context.
        await dbSet.AddAsync(model);
        
        // Save changes and get the affected rows count
        int affectedRows = await _dbContext.SaveChangesAsync();
        
        // Check if the operation was successful, if so return Success Response.
        if (affectedRows > 0)
            return DbResultBuilder.CreateResultSuccess(model);

        // if code reaches that point, return failed response.
        throw new DatabaseException("Couldn't add data into Database! No rows affected");
    }
    
    /// <inheritdoc />
    public async Task<DbQueryListResult<TModel>> GetAll()
    {
        // Get databaseSet for current entity, we dont need to track those since no modifications will be applied to them.
        var dbSet = _dbContext.Set<TModel>()
            .AsNoTracking();
        
        // Get all data for current table.
        var list = await dbSet.ToListAsync();
        
        // Return the list.
        return DbResultBuilder.ListResultSuccess(list);
    }
    
    /// <inheritdoc />
    public async Task<PagedResponse<TModel>> GetAll<TPagedRequest>(TPagedRequest pagedRequest) 
        where TPagedRequest : PagedRequest
    {
        // Get databaseSet for current entity, we dont need to track those since no modifications will be applied to them.
        var dbSet = _dbContext.Set<TModel>()
            .AsNoTracking();
        
        // Get the count of all data. Using Count function of dbSet directly to send single request and only get the row count no other columns which we wont need at that point. 
        var totalRecords = await dbSet.CountAsync();
        
        // Apply pagination with using IQueryable to gain performance over IEnumareble, so query will be executed at last point.
        // If performance will be issue later, try to change pagination system and maybe use cursor-based pagination.
        var list = dbSet.ToPagedList(pagedRequest.PageSize!.Value, pagedRequest.PageNumber!.Value);
        
        // WithData response object with pagination information.
        var pagedResponse = new PagedResponse<TModel>
        {
            PageNumber = pagedRequest.PageNumber!.Value,
            PageSize = pagedRequest.PageSize!.Value,
            RecordsCount = totalRecords,
            PageCount = (int)MathF.Ceiling(totalRecords / (float)pagedRequest.PageSize),
            Data = list
        };
        
        // return the data.
        return pagedResponse;
    }
    
    /// <inheritdoc />
    public async Task<DbQuerySingleResult<TModel>> GetById(TKey id)
    {
        // Get databaseSet for current entity, we dont need to track those since no modifications will be applied to them.
        var dbSet = _dbContext.Set<TModel>()
            .AsNoTracking();
        
        // Try to get First item with the given Id. We use FirstOrDefault methode to possibly gain performance over SingleOrDefault.
        var foundObject = await dbSet.FirstOrDefaultAsync(y => y.Id!.Equals(id));
        
        // If object was found, return Success result. 
        if (foundObject is not null)
            return DbResultBuilder.SingleResultSuccess(foundObject);
        
        // if code reaches to that point, return Failed response.
        throw new DataNotFoundException($"Record for given id ({id.ToString()}) was not found!");
    }
    
    /// <inheritdoc />
    public async Task<DbUpdateResult<TModel>> Update(TModel model)
    {
        // Get databaseSet for current entity.
        var dbSet = _dbContext.Set<TModel>();
        
        // Attach requested model into context and change it state to Modified.
        var dbTrackedModel = dbSet.Attach(model);
        dbTrackedModel.State = EntityState.Modified;
        
        // Save changes and get affected rows count.
        int affectedRows = await _dbContext.SaveChangesAsync();
        
        // If affected rows count is positive, operation was succesful so return Success Response.
        if (affectedRows > 0)
            return DbResultBuilder.UpdateResultSuccess(dbTrackedModel.Entity);
        
        // if code reaches that point, return failed response.
        throw new DatabaseException("Couldn't update data in Database! No rows affected");
    }
    
    /// <inheritdoc />
    public async Task<DbDeleteResult<TKey>> Delete(TModel model)
    {
        // Get databaseSet for current entity.
        var dbSet = _dbContext.Set<TModel>();
        
        // Try to get First item with the given Id. We use FirstOrDefault methode to possibly gain performance over SingleOrDefault.
        var foundObject = await dbSet.FirstOrDefaultAsync(y => y.Id!.Equals(model.Id));
        
        // If no object was found, return Failed resposne.
        if (foundObject is null)
            throw new DataNotFoundException($"Record for given id ({model.Id.ToString()}) was not found!");
        
        // Change IsDeleted prop value to true for Soft-Delete operation.
        foundObject.IsDeleted = true;
        
        // Save changes and get affected rows count.
        int affectedRows = await _dbContext.SaveChangesAsync();
        
        // If affected rows count is positive, operation was succesful so return Success Response.
        if (affectedRows > 0)
            return DbResultBuilder.DeleteResultSuccess(model.Id);
        
        // if code reaches that point, return failed response.
        throw new DatabaseException("Couldn't update data in Database! No rows affected");
    }
    
    /// <inheritdoc />
    public async Task<DbDeleteResult<TKey>> DeleteById(TKey id)
    {
        // Get databaseSet for current entity.
        var dbSet = _dbContext.Set<TModel>();
        
        // Try to get First item with the given Id. We use FirstOrDefault methode to possibly gain performance over SingleOrDefault.
        var foundObject = await dbSet.FirstOrDefaultAsync(y => y.Id!.Equals(id));
        
        // If no object was found, return Failed resposne.
        if (foundObject is null)
            throw new DataNotFoundException($"Record for given id ({id.ToString()}) was not found!");
        
        // Change IsDeleted prop value to true for Soft-Delete operation.
        foundObject.IsDeleted = true;
        
        // Save changes and get affected rows count.
        int affectedRows = await _dbContext.SaveChangesAsync();
        
        // If affected rows count is positive, operation was succesful so return Success Response.
        if (affectedRows > 0)
            return DbResultBuilder.DeleteResultSuccess(id);
        
        // if code reaches to that point, return Failed response.
        throw new DatabaseException("Couldn't update data in Database! No rows affected");
    }
    
    /// <inheritdoc />
    public async Task<DbQueryListResult<TModel>> Query(Expression<Func<TModel, bool>> query)
    {
        // Get databaseSet for current entity, we dont need to track those since no modifications will be applied to them.
        var dbSet = _dbContext.Set<TModel>()
            .AsNoTracking();
        
        // Apply the query into DbSet.
        var list = await dbSet.Where(query).ToListAsync();
        
        // Return the result as Success response.
        return DbResultBuilder.ListResultSuccess(list);
    }

    /// <inheritdoc />
    public async Task<PagedResponse<TModel>> Query<TPagedRequest>(Expression<Func<TModel, bool>> query, TPagedRequest pagedRequest) where TPagedRequest : PagedRequest
    {
        // Get databaseSet for current entity, we dont need to track those since no modifications will be applied to them.
        var dbSet = _dbContext.Set<TModel>()
            .AsNoTracking()
            .Where(query);
        
        // Get the count of all data. Using Count function of dbSet directly to send single request and only get the row count no other columns which we wont need at that point. 
        var totalRecords = await dbSet.CountAsync();
        
        // Apply pagination with using IQueryable to gain performance over IEnumareble, so query will be executed at last point.
        // If performance will be issue later, try to change pagination system and maybe use cursor-based pagination.
        var list = dbSet.ToPagedList(pagedRequest.PageSize!.Value, pagedRequest.PageNumber!.Value);
        
        // WithData response object with pagination information.
        var pagedResponse = new PagedResponse<TModel>
        {
            PageNumber = pagedRequest.PageNumber!.Value,
            PageSize = pagedRequest.PageSize!.Value,
            RecordsCount = totalRecords,
            PageCount = (int)MathF.Ceiling(totalRecords / (float)pagedRequest.PageSize),
            Data = list
        };
        
        // return the data.
        return pagedResponse;
    }
}