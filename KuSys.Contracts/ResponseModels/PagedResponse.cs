namespace KuSys.Contracts.ResponseModels;

/// <summary>
/// Base response type for paged responses
/// </summary>
public abstract class PagedResponse
    : BaseResponse
{
    /// <summary>
    /// Current page number.
    /// </summary>
    public int PageNumber { get; set; }
    
    /// <summary>
    /// Requested page size
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// Total records count.
    /// </summary>
    public int RecordsCount { get; set; }
    
    /// <summary>
    /// Max page count depends on requested PageSize.
    /// </summary>
    public int PageCount { get; set; }
}