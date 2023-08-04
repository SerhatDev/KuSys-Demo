namespace KuSys.Core.Types;

/// <summary>
/// Result builder to build ResponseTypes for database operations.
/// </summary>
public static class DbResultBuilder
{
    /// <summary>
    /// Response type for successful create operations.
    /// </summary>
    /// <param name="model">The object which will be created.</param>
    /// <typeparam name="TModel">Type of the object which will be created.</typeparam>
    /// <returns>Returns <see cref="DbCreateResult{T}"/></returns>
    public static DbCreateResult<TModel> CreateResultSuccess<TModel>(TModel model)
    {
        DbCreateResult<TModel> result = new DbCreateResult<TModel>(model, OperationResult.Success);
        return result;
    }
    
    /// <summary>
    /// Response type for failed create operations.
    /// </summary>
    /// <param name="errorMessage">Error Message. Defaults to empty string.</param>
    /// <typeparam name="TModel">The type of the object which couldn't be created.</typeparam>
    /// <returns></returns>
    public static DbCreateResult<TModel> CreateResultFailed<TModel>(string errorMessage = "")
    {
        DbCreateResult<TModel> result = new DbCreateResult<TModel>(default, OperationResult.Failed,errorMessage);
        return result;
    }

    /// <summary>
    /// Successful Response type for Get queries which expects a list of data.
    /// </summary>
    /// <param name="list">Result of the operation.</param>
    /// <typeparam name="TModel">The type of object which the list will be populated of.</typeparam>
    /// <returns>Returns <see cref="DbQueryListResult{T}"/></returns>
    public static DbQueryListResult<TModel> ListResultSuccess<TModel>(List<TModel> list)
    {
        DbQueryListResult<TModel> result = new DbQueryListResult<TModel>(list, OperationResult.Success);
        return result;
    }

    /// <summary>
    /// Successful Response type for Get queries which expects a single data.
    /// </summary>
    /// <param name="model">Result of the operation.</param>
    /// <typeparam name="TModel">The type of the data.</typeparam>
    /// <returns>Returns <see cref="DbQuerySingleResult{T}"/></returns>
    public static DbQuerySingleResult<TModel> SingleResultSuccess<TModel>(TModel model)
    {
        DbQuerySingleResult<TModel> result = new DbQuerySingleResult<TModel>(model, OperationResult.Success);
        return result;
    }
    /// <summary>
    /// Failed response type for Get queries which expects a single data.
    /// </summary>
    /// <typeparam name="TModel">The type of the data.</typeparam>
    /// <returns>Returns <see cref="DbQuerySingleResult{T}"/></returns>
    public static DbQuerySingleResult<TModel> SingleResultFailed<TModel>()
    {
        DbQuerySingleResult<TModel> result = new DbQuerySingleResult<TModel>(default(TModel), OperationResult.Failed);
        return result;
    }

    /// <summary>
    /// Failed response type for Update operations.
    /// </summary>
    /// <typeparam name="TModel">The type of the object which update operation failed.</typeparam>
    /// <returns>Returns <see cref="DbUpdateResult{T}"/></returns>
    public static DbUpdateResult<TModel> UpdateResultFailed<TModel>(string errorMessage="") =>
        new DbUpdateResult<TModel>(default(TModel), OperationResult.Failed,errorMessage);

    /// <summary>
    /// Successful response type for Update operations.
    /// </summary>
    /// <param name="model">Operation result data.</param>
    /// <typeparam name="TModel">Type of the data.</typeparam>
    /// <returns>Returns <see cref="DbUpdateResult{T}"/></returns>
    public static DbUpdateResult<TModel> UpdateResultSuccess<TModel>(TModel model) =>
        new DbUpdateResult<TModel>(model, OperationResult.Success);

    /// <summary>
    /// Failed response type for the Delete operation.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <returns>Returns <see cref="DbDeleteResult{T}"/></returns>
    public static DbDeleteResult<TKey> DeleteResultFailed<TKey>(string errorMessage="") =>
        new DbDeleteResult<TKey>(default, OperationResult.Failed,errorMessage);

    /// <summary>
    /// Successful response type for the Delete operation.
    /// </summary>
    /// <param name="deletedId">The id of the deleted data.</param>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <returns></returns>
    public static DbDeleteResult<TKey> DeleteResultSuccess<TKey>(TKey deletedId) =>
        new DbDeleteResult<TKey>(deletedId, OperationResult.Success);
}