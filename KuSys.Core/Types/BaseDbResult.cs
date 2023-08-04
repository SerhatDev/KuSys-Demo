namespace KuSys.Core.Types;

/// <summary>
/// Used to create DbResult objects.
/// </summary>
/// <typeparam name="T">Return type of the operation.</typeparam>
public abstract class BaseDbResult<T>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="data">Result of the operation.</param>
    /// <param name="result">Status of the operation.</param>
    /// <param name="errorMessage">Error message if the operation has failed.</param>
    public BaseDbResult(T data, OperationResult result, string errorMessage)
    {
        Data = data;
        Result = result;
        ErrorMessage = errorMessage;
    }
    
    /// <summary>
    /// Result of the operation.
    /// </summary>
    public T Data { get; }
    
    /// <summary>
    /// Status of the opreation.
    /// </summary>
    public OperationResult Result { get; }
    
    /// <summary>
    /// Error message if the operation has failed.
    /// </summary>
    public string ErrorMessage { get; set; }
}