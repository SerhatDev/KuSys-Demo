namespace KuSys.DataAccess.Repositories.Course;

/// <summary>
/// Course repository to manage Database Operations.
/// </summary>
public sealed class CourseRepository
    : BaseRepository<Entities.Course, Guid>, ICourseRepository
{
    /// <inheritdoc />
    public CourseRepository(KuSysDbContext dbContext) 
        : base(dbContext)
    {
    }
}