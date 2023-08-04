namespace KuSys.Core.Types;

/// <summary>
/// Response type for Update operations.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class DbUpdateResult<T>
    : BaseDbResult<T>
{
    /// <inheritdoc />
    public DbUpdateResult(T data, OperationResult result,string errorMessage="") 
        : base(data, result,errorMessage)
    {
    }
}