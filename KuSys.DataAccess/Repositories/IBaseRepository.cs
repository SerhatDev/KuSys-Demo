using System.Linq.Expressions;
using KuSys.Core.Types;

namespace KuSys.DataAccess.Repositories;

public interface IBaseRepository<TModel, TKey>
{
    Task<DbCreateResult<TModel>> AddNew(TModel model);
    Task<DbQueryListResult<TModel>> GetAll();

    Task<PagedResponse<TModel>> GetAll<TPagedRequest>(TPagedRequest pagedRequest) where TPagedRequest : PagedRequest;

    Task<DbQuerySingleResult<TModel>> GetById(TKey id);
    Task<DbUpdateResult<TModel>> Update(TModel model);
    Task<DbDeleteResult<TKey>> Delete(TModel model);
    Task<DbDeleteResult<TKey>> DeleteById(TKey id);

    Task<DbQueryListResult<TModel>> Query(Expression<Func<TModel, bool>> query);
}