using KuSys.Core;
using KuSys.Core.Exceptions;
using KuSys.Core.Types;
using KuSys.DataAccess.Repositories.Student;
using KuSys.DataAccess.Repositories.User;
using KuSys.Entities;
using KuSys.Entities.Requests;
using KuSys.Entities.TypeMappings;

namespace KuSys.Services;

/// <summary>
/// Service to manage stundets.
/// </summary>
public sealed class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Consturoctor. Please use Dependencjy Injection.
    /// </summary>
    /// <param name="studentRepository">Student Repository</param>
    /// <param name="userRepository">User repostiroy</param>
    public StudentService(IStudentRepository studentRepository,IUserRepository userRepository)
    {
        _studentRepository = studentRepository;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Update Student with given values.
    /// </summary>
    /// <param name="id">Id of the student</param>
    /// <param name="requestModel">New values</param>
    /// <returns><see cref="DbUpdateResult{T}"/></returns>
    public async Task<DbUpdateResult<Student>> UpdateStudent(Guid id,UpdateStudentModel requestModel)
    {   
        // Get student with given Id
        var dbStudent = await _studentRepository.GetById(id);
        
        // If student was not fount, return Error response
        if (dbStudent.Result == OperationResult.Failed)
            throw new DataNotFoundException($"Student with provided id ({id.ToString()}) was not found");
        
        // Set new values
        dbStudent.Data!.FirstName = requestModel.FirstName;
        dbStudent.Data.LastName = requestModel.LastName;
        dbStudent.Data.Gender = requestModel.Gender;
        dbStudent.Data.BirthDate = requestModel.BirthDate;
        
        // Update the student with new values
        var updateResult= await _studentRepository.Update(dbStudent.Data);
        
        // if Update operation has been failed, return error response.
        if (updateResult.Result == OperationResult.Failed)
            throw new DatabaseException("Couldn't update user!");
        
        // If the code reaches that point, return success result with update student entity.
        return new DbUpdateResult<Student>(dbStudent.Data, OperationResult.Success);
    }

    /// <summary>
    /// Delete student with given Id.
    /// </summary>
    /// <param name="id">Id of the student</param>
    /// <returns><see cref="DbDeleteResult{T}"/></returns>
    public async Task<DbDeleteResult<Guid>> DeleteStudentById(Guid id)
    {
        // Delete student from Database via studentRepository.
        var data = await _studentRepository.DeleteById(id);
        
        // return result.
        return data;
    }

    /// <summary>
    /// Get student by Id.
    /// </summary>
    /// <param name="id">Student Id</param>
    /// <returns><see cref="GetStudentResponse"/></returns>
    public async Task<GetStudentResponse> GetStudentById(Guid id)
    {
        var student = await _studentRepository.GetById(id);
        if (student is null)
            throw new DataNotFoundException($"Student with given id({id}) was not found!");
        return student.Data.ToResponse();
    }

    /// <summary>
    /// Add new student with given values.
    /// </summary>
    /// <param name="requestModel">Student data.</param>
    /// <returns><see cref="AddStudentResponse"/></returns>
    public async Task<AddStudentResponse> AddStudent(AddStudentRequestModel requestModel)
    {
        // Convert Request model to user entity
        var userModel = requestModel.ToUserEntity();
        
        // Firstly, create a user for the student so we can create relation later.
        var dbUser = await _userRepository.AddStudentUser(userModel, requestModel.Password);
        
        // If create user operation has been failed, return Error response.
        if (dbUser.Result == OperationResult.Failed)
            throw new DatabaseException(dbUser.ErrorMessage);

        // Convert Request model to user entity
        var studentEntity = requestModel.ToEntity();
        
        // Assign user id with newly created user
        studentEntity.UserId = dbUser.Data!.Id;
        
        // Create new student
        var addStudentResult = await _studentRepository.AddNew(studentEntity);
        
        // If the create stundet operation has been failed, return Error response.
        if (addStudentResult.Result == OperationResult.Failed)
            throw new DatabaseException(addStudentResult.ErrorMessage);

        // If code reaches to that point, return Success response with newly created User Id.
        return new AddStudentResponse()
        {
            IsSuccess = true,
            Id = addStudentResult.Data!.Id
        };
    }

    /// <summary>
    /// Get All students with pagination.
    /// </summary>
    /// <param name="requestModel">Paged Request</param>
    /// <returns><see cref="PagedResponse{T}"/></returns>
    public async Task<PagedResponse<Student>> GetAll(GetAllStudentRequest requestModel)
    {
        // Get paged response for requested page
        var data = await _studentRepository.GetAll(requestModel);
        
        // return data
        return data;
    }

    /// <summary>
    /// Get all students with their joined course information.
    /// </summary>
    /// <returns><see cref="DbQueryListResult{T}"/></returns>
    public async Task<DbQueryListResult<StudentsWithCourseResponse>> GetStudentsWithCourses()
    {
        // Get students with thier joined course information
        var data = await _studentRepository.StudentsWithCourses();
        
        // return data
        return data;
    }
}