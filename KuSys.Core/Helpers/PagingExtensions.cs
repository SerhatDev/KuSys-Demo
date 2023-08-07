namespace KuSys.Core.Helpers;

/// <summary>
/// Extensions for paging operation.
/// </summary>
public static class PagingExtensions
{
    /// <summary>
    /// Apply paging to the set.
    /// </summary>
    /// <param name="queryable">IQueryable object to apply paging</param>
    /// <param name="pageSize">Requested page size</param>
    /// <param name="pageNumber">Number of the requested page</param>
    /// <typeparam name="T">Type of the data</typeparam>
    /// <returns>List of requested data type</returns>
    public static List<T> ToPagedList<T>(this IQueryable<T> queryable, int pageSize, int pageNumber)
    {
        // Apply pagination with using IQueryable to gain performance over IEnumareble, so query will be executed at last point.
        // If performance will be issue later, try to change pagination system and maybe use cursor-based pagination.
        return queryable
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
}