namespace KuSys.Core.Types;

/// <summary>
/// Return type for adding new data into database operation.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class DbCreateResult<T>
    : BaseDbResult<T>
{
    /// <summary>
    /// Id of newly created object.
    /// </summary>
    public Guid Id { get; set; }

    /// <inheritdoc />
    public DbCreateResult(T data, OperationResult result,string errorMessage="") 
        : base(data, result,errorMessage)
    {
    }
}