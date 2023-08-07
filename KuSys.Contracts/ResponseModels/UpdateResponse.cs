namespace KuSys.Contracts.ResponseModels;

/// <summary>
/// Update api response model.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class UpdateResponse<T>
    : BaseResponse
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="id"></param>
    public UpdateResponse(T id)
    {
        Id = id;
        IsSuccess = true;
    }

    /// <summary>
    /// Id of the object.
    /// </summary>
    public T Id { get; set; }
}