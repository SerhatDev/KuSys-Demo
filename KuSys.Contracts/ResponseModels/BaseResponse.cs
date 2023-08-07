namespace KuSys.Contracts.ResponseModels;

/// <summary>
/// Base class for Api/Service responses.
/// </summary>
public abstract class BaseResponse
{
    /// <summary>
    /// Operation status
    /// </summary>
    public bool IsSuccess { get; set; } = true;
}