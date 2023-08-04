namespace KuSys.Core.Types;

/// <summary>
/// Base type of PagedRequests.
/// </summary>
public abstract class PagedRequest
{
    /// <summary>
    /// Requested Page Size
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// Requested Page Number.
    /// </summary>
    public int PageNumber { get; set; }
}