namespace KuSys.Contracts.RequestModels;

/// <summary>
/// Paged Request
/// </summary>
public abstract class PagedRequest
{
    private int? _pageNumber;
    private int? _pageSize;

    /// <summary>
    /// Requested Page Number (Set to 1 as default if no value provided)
    /// </summary>
    public int? PageNumber
    {
        get => _pageNumber ?? 1; // Return 1 if pageNumber is null
        set => _pageNumber = value;
    }

    /// <summary>
    /// Requested Page Size  (Set to 10 as default if no value provided)
    /// </summary>
    public int? PageSize
    {
        get => _pageSize ?? 10; // Return 10 if pageSize is null
        set => _pageSize = value;
    }
}
