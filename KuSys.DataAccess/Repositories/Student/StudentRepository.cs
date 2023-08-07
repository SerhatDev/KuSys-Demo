using KuSys.Contracts.RequestModels;
using KuSys.Contracts.ResponseModels;
using KuSys.Core;
using KuSys.Core.Helpers;
using KuSys.Core.Types;
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
    public async Task<PagedResponse<StudentsWithCoursesResponse>> StudentsWithCourses(GetStudentsWithCoursesRequest request)
    {
        
        // Get databaseSet for current entity, we dont need to track those since no modifications will be applied to them.
        var dbSet = _dbContext.Set<Entities.Student>()
            .Include(x => x.StudentCourses)
            .ThenInclude(x => x.Course)
            .AsNoTracking();
        
        // Get the count of all data. Using Count function of dbSet directly to send single request and only get the row count no other columns which we wont need at that point. 
        var totalRecords = await dbSet.CountAsync();
        
        // Apply pagination with using IQueryable to gain performance over IEnumareble, so query will be executed at last point.
        // If performance will be issue later, try to change pagination system and maybe use cursor-based pagination.
        var list = dbSet.ToPagedList(request.PageSize!.Value, request.PageNumber!.Value).Select(x => new StudentsWithCoursesResponse()
        {
            Student = new StudentResponseModel()
            {
                BirthDate = x.BirthDate,
                FirstName = x.FirstName,
                Gender = (Gender)x.Gender,
                Id = x.Id,
                LastName = x.LastName
            },
            Courses = x.StudentCourses.Select(y => new CourseResponseModel()
            {
                Code = y.Course.Code,
                Name = y.Course.Name,
                Id = y.Course.Id
            }).ToList()
        }).ToList();
        
        
        // WithData response object with pagination information.
        var pagedResponse = new PagedResponse<StudentsWithCoursesResponse>
        {
            PageNumber = request.PageNumber!.Value,
            PageSize = request.PageSize!.Value,
            RecordsCount = totalRecords,
            PageCount = (int)MathF.Ceiling(totalRecords / (float)request.PageSize),
            Data = list
        };
        
        // return the data.
        return pagedResponse;
    }
}