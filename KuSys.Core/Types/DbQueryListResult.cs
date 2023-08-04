namespace KuSys.Core.Types;

/// <summary>
/// Return type of getting a list of data from database.
/// </summary>
/// <typeparam name="T">The type which list will be created of.</typeparam>
public sealed class DbQueryListResult<T>
    : BaseDbResult<List<T>>
{
    /// <inheritdoc />
    public DbQueryListResult(List<T> data, OperationResult result,string errorMessage="") 
        : base(data, result,errorMessage)
    {
    }
}