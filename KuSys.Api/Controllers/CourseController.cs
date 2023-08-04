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
    /// Get All active courses. ***(ADMIN ONLY) US-1***
    /// </summary>
    /// <returns>Returns List of Active Courses</returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ClaimRequirement("US-1")]
    [ProducesResponseType(typeof(DbQueryListResult<Course>),200)]
    public async Task<IActionResult> GetAllCourses()
    {
        var data = await _courseService.GetAll();
        return Ok(data);
    }

    /// <summary>
    /// Create new Course.
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    [HttpPost]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    // [ClaimRequirement("US-1")]
    [ProducesResponseType(typeof(DbCreateResult<Course>),200)]
    [ProducesResponseType(400)]
    [Consumes( typeof(AddNewCourseRequestModel),"text/json")]
    public async Task<IActionResult> AddNewCourse(AddNewCourseRequestModel requestModel)
    {
        var data = await _courseService.AddNewCourse(requestModel);
        return data.Result == OperationResult.Success
            ? Ok(data.Data)
            : BadRequest();
    }
}