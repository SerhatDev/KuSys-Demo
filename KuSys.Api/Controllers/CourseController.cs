using KuSys.Contracts.RequestModels;
using KuSys.Contracts.ResponseModels;
using KuSys.Core;
using KuSys.Core.AttributeFilters;
using KuSys.Services;
using KuSys.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KuSys.Api.Controllers;

/// <summary>
/// Course controller to manage Courses.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class CourseController : ControllerBase
{
    // Course service to manage course operations.
    private readonly ICourseService _courseService;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="courseService"></param>
    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    /// <summary>
    /// Get FullAccess active courses. ***(ADMIN ONLY) US-1***
    /// </summary>
    /// <returns>Returns List of Active Courses</returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ClaimRequirement("US-1", PermissionType.FullAccess)]
    [ProducesResponseType(typeof(CourseListResponseModel), 200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [Consumes(typeof(GetCoursesRequestModel), "text/json")]
    [ValidateRequest]
    public async Task<IActionResult> GetAllCourses([FromQuery] GetCoursesRequestModel request)
    {
        // Get all courses.
        var serviceResponse = await _courseService.GetAll(request);

        // Create api response from serviceResponse and return to the user.
        return ApiResponse.WithData(serviceResponse);
    }

    /// <summary>
    /// WithData new Course.
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ClaimRequirement("US-1")]
    [ProducesResponseType(typeof(CourseResponseModel), 201)]
    [ProducesResponseType(400)]
    [Consumes(typeof(NewCourseRequestModel), "text/json")]
    [ValidateRequest]
    public async Task<IActionResult> AddNewCourse(NewCourseRequestModel requestModel)
    {
        // Add new course
        var data = await _courseService.AddNewCourse(requestModel);

        // Create api response from serviceResponse and return.
        return ApiResponse.Created(this, data);
    }
}