using KuSys.Contracts.RequestModels;
using KuSys.Contracts.ResponseModels;
using KuSys.Core;
using KuSys.Core.AttributeFilters;
using KuSys.Core.Types;
using KuSys.Entities;
using KuSys.Services;
using KuSys.Services.Interfaces;
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
    [ProducesResponseType(typeof(StudentListResponse),200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [Consumes(typeof(GetStudentsRequest),"text/json")]
    [ValidateRequest]
    public async Task<IActionResult> GetAllStudents([FromQuery]GetStudentsRequest request)
    {
        // Get all active students via studentService
        var data = await _studentService.GetAll(request);
        
        // Return retrieved data with OkResponse.
        return ApiResponse.WithData(data);
    }

    /// <summary>
    /// Add new student. This will also generate a user for the student and assign to Student Role.  ***(ADMIN ONLY) US-1***
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ClaimRequirement("US-1")]
    [ProducesResponseType(typeof(NewStudentResponseModel),200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [Consumes(typeof(NewStudentRequestModel),"text/json")]
    [ValidateRequest]
    public async Task<IActionResult> AddStudent(NewStudentRequestModel request)
    {
        // Try to add new student and user for the student.
        var data = await _studentService.AddStudent(request);
        
        // Return result
        return ApiResponse.Created(this, data);
    }

    /// <summary>
    /// Get Student information with given StudentId. *** Admins can get any student by id while students can only get their own information ***
    /// </summary>
    /// <param name="id">Student Id</param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ClaimRequirement("US-1")]
    [ProducesResponseType(typeof(StudentResponseModel),200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<IActionResult> GetStudentById(Guid id)
    {
        // Get student information for given studentId.
        var serviceResponse = await _studentService.GetStudentById(id);
        
        // Return found Student object
        return ApiResponse.WithData(serviceResponse);
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
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<IActionResult> RemoveStudentById(Guid id)
    {
        // Soft delete student with given StudentId via StudentService.
        var data = await _studentService.DeleteStudentById(id);
        
        // Return 204 status if operations was successful, otherwise return BadRequest with errorMessage.
        return ApiResponse.Deleted(data);
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
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [Consumes(typeof(UpdateStudentModel),"text/json")]
    [ValidateRequest]
    public async Task<IActionResult> UpdateStudent(Guid id,UpdateStudentModel requestModel)
    {
        // Update specified student with specified values via StudentService.
        var serviceResponse = await _studentService.UpdateStudent(id,requestModel);
        
        // Return 200 status with operation data if operation was successful otherwise return BadRequest with errorMessage.
        return ApiResponse.Updated(serviceResponse);
    }

    /// <summary>
    /// Get available course list for Specified Student.
    /// </summary>
    /// <param name="studentId">StudentId</param>
    /// <param name="request">Paging Information.</param>
    /// <returns></returns>
    [HttpGet("{studentId:guid}/available-courses")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CourseListResponseModel),200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ValidateRequest]
    public async Task<IActionResult> AvailableCourses(Guid studentId,[FromQuery] AvailableCoursesRequest request)
    {
        // Get the list of courses for specified student which the student wasn't matched yet.
        var serviceResponse = await _studentCourseService.GetAvailableCourses(studentId,request);
        
        // Return the data from the service with 200 status code.
        return ApiResponse.WithData(serviceResponse);
    }

    /// <summary>
    /// Get the list of courses in which the specified student has been matched.
    /// </summary>
    /// <param name="studentId">Student Id</param>
    /// <param name="request">Paging Informaiton</param>
    /// <returns></returns>
    [HttpGet("{studentId:guid}/courses")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CourseListResponseModel),200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ValidateRequest]
    public async Task<IActionResult> JointCourses(Guid studentId,[FromQuery] JoinedCoursesRequest request)
    {
        // Get the list of courses in which the specified student has been matched.
        var serviceResponse = await _studentCourseService.GetJoinedCourses(studentId,request);
        
        // Return the list with 200 status code.
        return ApiResponse.WithData(serviceResponse);
    }

    /// <summary>
    /// Assign a course to a student. A student only can be assigned to a course which she wasn't assigned already.  ***(ADMIN ONLY) US-1***
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("{studentId:guid}/Courses")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ClaimRequirement("US-1")]
    [ProducesResponseType(typeof(SelectCourseResponseModel),200)]
    [ProducesResponseType(typeof(string),400)]
    [ProducesResponseType(typeof(string),401)]
    [ProducesResponseType(typeof(string),403)]
    [Consumes(typeof(SelectCourseRequest),"text/json")]
    [ValidateRequest]
    public async Task<IActionResult> SelectCourse(Guid studentId, SelectCourseRequest request)
    {
        // Assign specified student to specified course. This will throw an error if student was matched with that course before.
        var serviceResponse = await _studentCourseService.SelectCourse(studentId, request);
        
        // Return data with 200 status code if the operation was successful otherwise return BadRequest.
        return ApiResponse.Created(this, serviceResponse);
    }

    /// <summary>
    /// Get students with their course info.  ***(ADMIN ONLY) US-1***
    /// </summary>
    /// <returns></returns>
    [HttpGet("Courses")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ClaimRequirement("US-1")]
    [ProducesResponseType(typeof(StudentWithCoursesListResponse),200)]
    [ProducesResponseType(typeof(string),400)]
    [ProducesResponseType(typeof(string),401)]
    [ProducesResponseType(typeof(string),403)]
    [ValidateRequest]
    public async Task<IActionResult> GetStudentsWithCourses([FromQuery] GetStudentsWithCoursesRequest request)
    {
        // Get the list of students with their course info.
        var data = await _studentService.GetStudentsWithCourses(request);
        
        // Return data with 200 status code if operation was successful otherwise return BadRequest.
        return ApiResponse.WithData(data);
    }
}