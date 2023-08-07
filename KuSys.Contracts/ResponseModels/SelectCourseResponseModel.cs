namespace KuSys.Contracts.ResponseModels;

/// <summary>
/// Response type for selecting new course for a student.
/// </summary>
public sealed class SelectCourseResponseModel
    : BaseResponse
{
    /// <summary>
    /// Initialize.
    /// </summary>
    /// <param name="id"></param>
    public SelectCourseResponseModel(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }
}