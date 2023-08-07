namespace KuSys.Core.Types;

/// <summary>
/// Base response type for PagedRequests.
/// </summary>
/// <typeparam name="T">Type of the object which the list will be populated.</typeparam>
public class PagedResponse<T>
{
    /// <summary>
    /// Requested Page Size.
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// Current Page Number
    /// </summary>
    public int PageNumber { get; set; }
    
    /// <summary>
    /// Total records count without paging applied.
    /// </summary>
    public int RecordsCount { get; set; }
    
    /// <summary>
    /// Last page number.
    /// </summary>
    public int PageCount { get; set; }
    
    /// <summary>
    /// Requested data
    /// </summary>
    public List<T> Data { get; set; }
}