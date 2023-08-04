namespace KuSys.Core.Types;

/// <summary>
/// Return type of soft deleting data from database.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class DbDeleteResult<T>
    : BaseDbResult<T>
{
    /// <inheritdoc />
    public DbDeleteResult(T data, OperationResult result,string errorMessage="") 
        : base(data, result,errorMessage)
    {
    }
}