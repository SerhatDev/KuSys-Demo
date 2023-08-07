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
/// Authentication controller for the System.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    // Inject userService to manage user operations.
    private readonly IUserService _userService;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userService">User Service.</param>
    public AuthController(IUserService userService)
    {
        _userService = userService;
    }


    /// <summary>
    /// Register new user with no roles. You can use "make-admin" endpoint to give user "Admin" role.
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns>Returns Id of newly created User.</returns>
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(RegisterResponseModel),200)]
    [ProducesResponseType(typeof(RegisterResponseModel),400)]
    [Consumes(typeof(RegisterRequestModel),"text/json")]
    [ValidateRequest]
    public async Task<IActionResult> Register(RegisterRequestModel requestModel)
    {
        // WithData user with provided data via userService.
        var serviceResponse = await _userService.CreateUser(requestModel);

        // Return the response of the service.
        return ApiResponse.WithData(serviceResponse);
    }


    /// <summary>
    /// Login endpoint. Users can login with their Email and password to get access token.
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns>Returns Access token.</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseModel),200)]
    [Consumes(typeof(LoginRequestModel),"text/json")]
    [ValidateRequest]
    public async Task<IActionResult> Login(LoginRequestModel requestModel)
    {
        // Try to log user in with provided information.
        var serviceResponse = await _userService.Login(requestModel);

        // Return the response of the service.
        return ApiResponse.WithData(serviceResponse);
    }

    /// <summary>
    /// This endpoint specifically created for test purposes. Use this endpoint to assign admin role to any user.
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns>Returns 200 if operation was successful</returns>
    [HttpPost("make-admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(400)]
    [Consumes(typeof(LoginRequestModel),"text/json")]
    [ValidateRequest]
    public async Task<IActionResult> MakeAdmin(LoginRequestModel requestModel)
    {
        // Try to assign admin role to the user which specified in request.
        var serviceResponse = await _userService.MakeAdmin(requestModel);
        
        // Depends on operation status, Return OkResult or BadRequest
        return serviceResponse
            ? Ok()
            : BadRequest();
    }
    
    /// <summary>
    /// This endpoint specifically created for test purposes. Use this endpoint to assign Student role to any user.
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    [HttpPost("make-student")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(bool),200)]
    [ProducesResponseType(400)]
    [Consumes(typeof(LoginRequestModel),"text/json")]
    [ValidateRequest]
    public async Task<IActionResult> MakeStudent(LoginRequestModel requestModel)
    {
        // Try to assign Student role to the user which specified in request.
        var serviceResponse = await _userService.MakeAdmin(requestModel);
        
        // DEpends on operation status, Return OkResult or BadRequest
        return serviceResponse
            ? Ok()
            : BadRequest();
    }
}