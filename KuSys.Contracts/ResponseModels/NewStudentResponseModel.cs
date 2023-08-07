namespace KuSys.Contracts.ResponseModels;

/// <summary>
/// Response type for AddNewStudent operation.
/// </summary>
public sealed class NewStudentResponseModel
    : BaseResponse
{

    /// <summary>
    /// Id of the newly created user.
    /// </summary>
    public Guid Id { get; set; }
}