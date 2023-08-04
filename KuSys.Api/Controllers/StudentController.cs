using KuSys.Core;
using KuSys.Core.AttributeFilters;
using KuSys.Core.Types;
using KuSys.Entities;
using KuSys.Entities.Requests;
using KuSys.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KuSys.Api.Controllers;

/// <summary>
/// Student Endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    // Student Service to manage student operations.
    private readonly IStudentService _studentService;
    
    // StudentCourse Service to manage StudentCourse operations.
    private readonly IStudentCourseService _studentCourseService;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="studentService">StudentService injection.</param>
    /// <param name="studentCourseService">StudentCourseService injection.</param>
    public StudentController(IStudentService studentService,IStudentCourseService studentCourseService)
    {
        _studentService = studentService;
        _studentCourseService = studentCourseService;
    }

    /// <summary>
    /// Get all Students with their personal information. ***(ADMIN ONLY) US-1***
    /// </summary>
    /// <param name="request">Specify paging information.</param>
    /// <returns>Returns paged list of Students.</returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ClaimRequirement("US-1")]
    [ProducesResponseType(typeof(PagedResponse<Student>),200)]
    [Consumes(typeof(GetAllStudentRequest),"text/json")]
    public async Task<IActionResult> GetAllStudents([FromQuery]GetAllStudentRequest request)
    {
        // Get all active students via studentService
        var data = await _studentService.GetAll(request);
        
        // Return retrieved data with OkResponse.
        return Ok(data);
    }

    /// <summary>
    /// Add new student. This will also generate a user for the student and assign to Student Role.  ***(ADMIN ONLY) US-1***
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ClaimRequirement("US-1")]
    [ProducesResponseType(typeof(AddStudentResponse),200)]
    [Consumes(typeof(AddStudentRequestModel),"text/json")]
    public async Task<IActionResult> AddStudent(AddStudentRequestModel request)
    {
        // Try to add new student and user for the student.
        var data = await _studentService.AddStudent(request);
        
        // Return result
        return Ok(data);
    }

    /// <summary>
    /// Get Student information with given StudentId. ***(ADMIN ONLY) US-1***
    /// </summary>
    /// <param name="id">Student Id</param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ClaimRequirement("US-1")]
    [ProducesResponseType(typeof(GetStudentResponse),200)]
    public async Task<IActionResult> GetStudentById(Guid id)
    {
        // Get student information for given studentId.
        var data = await _studentService.GetStudentById(id);
        
        // Return found Student object
        return Ok(data);
    }

    /// <summary>
    /// Delete student with given StudentId. --IT WILL SOFT DELETE--  ***(ADMIN ONLY) US-1***
    /// </summary>
    /// <param name="id">Student Id</param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ClaimRequirement("US-1")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(string),400)]
    public async Task<IActionResult> RemoveStudentById(Guid id)
    {
        // Soft delete student with given StudentId via StudentService.
        var data = await _studentService.DeleteStudentById(id);
        
        // Return 204 status if operations was succesfull, otherwise return BadRequest with errorMessage.
        return data.Result == OperationResult.Success
            ? NoContent()
            : BadRequest(data.ErrorMessage);
    }

    /// <summary>
    /// Update student with specified information. ***(ADMIN ONLY) US-1***
    /// </summary>
    /// <param name="id">Student Id</param>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    [HttpPatch("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ClaimRequirement("US-1")]
    [ProducesResponseType(typeof(DbUpdateResult<Student>),200)]
    [ProducesResponseType(typeof(string),400)]
    [Consumes(typeof(UpdateStudentModel),"text/json")]
    public async Task<IActionResult> UpdateStudent(Guid id,UpdateStudentModel requestModel)
    {
        // Update specified student with specified values via StudentService.
        var data = await _studentService.UpdateStudent(id,requestModel);
        
        // Return 200 status with operation data if operation was successful otherwise return BadRequest with errorMessage.
        return data.Result == OperationResult.Success
            ? Ok(data)
            : BadRequest(data.ErrorMessage);
    }

    /// <summary>
    /// Get available course list for Specified Student.
    /// </summary>
    /// <param name="studentId">StudentId</param>
    /// <returns></returns>
    [HttpGet("{studentId:guid}/available-courses")]
    [ProducesResponseType(typeof(List<CoursesResponse>),200)]
    public async Task<IActionResult> AvailableCourses(Guid studentId)
    {
        // Get the list of courses for specified student which the student wasn't matched yet.
        var data = await _studentCourseService.GetAvailableCourses(studentId);
        
        // Return the data from the service with 200 status code.
        return Ok(data);
    }

    /// <summary>
    /// Get the list of courses in which the specified student has been matched.
    /// </summary>
    /// <param name="studentId">Student Id</param>
    /// <returns></returns>
    [HttpGet("{studentId:guid}/courses")]
    [ProducesResponseType(typeof(List<CoursesResponse>),200)]
    public async Task<IActionResult> JointCourses(Guid studentId)
    {
        // Get the list of courses in which the specified student has been matched.
        var data = await _studentCourseService.GetJoinedCourses(studentId);
        
        // Return the list with 200 status code.
        return Ok(data);
    }

    /// <summary>
    /// Assign a course to a student. A student only can be assigned to a course which she wasn't assigned already.  ***(ADMIN ONLY) US-1***
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("{studentId:guid}/courses")]
    [ClaimRequirement("US-1")]
    [ProducesResponseType(typeof(DbCreateResult<StudentCourse>),200)]
    [ProducesResponseType(typeof(string),400)]
    [Consumes(typeof(SelectCourseRequest),"text/json")]
    public async Task<IActionResult> SelectCourse(Guid studentId, SelectCourseRequest request)
    {
        // Assign specified student to specified course. This will throw an error if student was matched with that course before.
        var data = await _studentCourseService.SelectCourse(studentId, request);
        
        // Return data with 200 status code if the operation was successful otherwise return BadRequest.
        return data.Result == OperationResult.Success
            ? Ok(data)
            : BadRequest(data.ErrorMessage);
    }

    /// <summary>
    /// Get students with their course info.  ***(ADMIN ONLY) US-1***
    /// </summary>
    /// <returns></returns>
    [HttpGet("courses")]
    [ClaimRequirement("US-1")]
    [ProducesResponseType(typeof(DbQueryListResult<StudentsWithCourseResponse>),200)]
    [ProducesResponseType(typeof(string),400)]
    public async Task<IActionResult> GetStudentsWithCourses()
    {
        // Get the list of students with their course info.
        var data = await _studentService.GetStudentsWithCourses();
        
        // Return data with 200 status code if operation was successful otherwise return BadRequest.
        return data.Result == OperationResult.Success
            ? Ok(data)
            : BadRequest(data.ErrorMessage);
    }
}