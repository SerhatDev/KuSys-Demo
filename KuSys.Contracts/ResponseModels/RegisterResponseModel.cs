namespace KuSys.Contracts.ResponseModels;

/// <summary>
/// Response model for register operation.
/// </summary>
public sealed class RegisterResponseModel 
    : BaseResponse
{
    public Guid Id { get; set; }

    private RegisterResponseModel(bool isSuccess, Guid id)
    {
        this.IsSuccess = isSuccess;
        this.Id = id;
    }

    private RegisterResponseModel(bool isSuccess)
    {
        this.IsSuccess = isSuccess;
    }

    /// <summary>
    /// Get failed register operation response type.
    /// </summary>
    /// <returns></returns>
    public static RegisterResponseModel Fail() => new(false);

    /// <summary>
    /// Get succeeded operation response type.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static RegisterResponseModel Success(Guid id) => new(true, id);
}