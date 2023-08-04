using KuSys.Core;
using KuSys.Core.Types;
using KuSys.Entities;
using KuSys.Entities.Requests;
using KuSys.Services;
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
    [ProducesResponseType(typeof(DbCreateResult<Guid>),200)]
    public async Task<IActionResult> Register(RegisterUserRequestModel requestModel)
    {
        // Create user with provided data via userService.
        var serviceResponse = await _userService.CreateUser(requestModel);
        
        // Depends on operation status, Return OkResult or BadRequest
        return serviceResponse.Result == OperationResult.Success
            ? Ok(serviceResponse.Data)
            : BadRequest();
    }


    /// <summary>
    /// Login endpoint. Users can login with their Email and password to get access token.
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns>Returns Access token.</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(string),200)]
    public async Task<IActionResult> Login(LoginRequestModel requestModel)
    {
        // Try to log user in with provided information.
        var res = await _userService.Login(requestModel);

        // Depends on operation status, Return OkResult or BadRequest
        return res.IsSuccessfull
            ? Ok(res.Token)
            : BadRequest(res.ErrorMessage);  
    }

    /// <summary>
    /// This endpoint specifically created for test purposes. Use this endpoint to assign admin role to any user.
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns>Returns 200 if operation was successfull</returns>
    [HttpPost("make-admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
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
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
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