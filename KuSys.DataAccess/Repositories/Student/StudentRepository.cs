using KuSys.Core.Types;
using KuSys.Entities.TypeMappings;
using Microsoft.EntityFrameworkCore;

namespace KuSys.DataAccess.Repositories.Student;

/// <summary>
/// Student repository to manage Database operations.
/// </summary>
public sealed class StudentRepository
    : BaseRepository<Entities.Student, Guid>, IStudentRepository
{
    private readonly KuSysDbContext _dbContext;

    /// <inheritdoc />
    public StudentRepository(KuSysDbContext dbContext) 
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Get the list of students with thier matched courses information.
    /// </summary>
    /// <returns>Returns <see cref="DbQueryListResult{StudentsWithCourseResponse}"/></returns>
    public async Task<DbQueryListResult<StudentsWithCourseResponse>> StudentsWithCourses()
    {
        // Get the all students and include required relations via include function and eager load them.
        // By using eager loading, the database will fetch all the required data in a single query
        var dbSet = _dbContext.Set<Entities.Student>()
            .Include(x => x.StudentCourses)
            .ThenInclude(x => x.Course);

        // Map data to Response model. if performance will be issue later, we might try to use anonymous type for select clause first and map later
        // (Need to check both SQL statements of lambda expressions to see if we can get rid of subqueries)
        var list = await dbSet.Select(x => new StudentsWithCourseResponse()
        {
            Student = x.ToResponse(),
            Courses = x.StudentCourses.Select(y => y.Course.ToResponse()).ToList()
        }).ToListAsync();
        return DbResultBuilder.ListResultSuccess(list);
    }
}