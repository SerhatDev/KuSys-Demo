namespace KuSys.Contracts.ResponseModels;

/// <summary>
/// Delete api response model.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class DeleteResponse<T>
    : BaseResponse
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="id"></param>
    public DeleteResponse(T id)
    {
        Id = id;
        IsSuccess = true;
    }

    /// <summary>
    /// Id of the object.
    /// </summary>
    public T Id { get; set; }
}