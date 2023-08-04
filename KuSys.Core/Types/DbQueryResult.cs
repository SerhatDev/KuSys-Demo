namespace KuSys.Core.Types;

/// <summary>
/// Result type for getting single object from database.
/// </summary>
/// <typeparam name="T">Type of the object</typeparam>
public sealed class DbQuerySingleResult<T>
    : BaseDbResult<T>
{
    /// <inheritdoc />
    public DbQuerySingleResult(T data, OperationResult result,string errorMessage="") 
        : base(data, result,errorMessage)
    {
    }
}