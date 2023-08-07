using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KuSys.Contracts.RequestModels;
using KuSys.Contracts.ResponseModels;
using KuSys.Core;
using KuSys.Core.Constants;
using KuSys.Core.Exceptions;
using KuSys.Core.Types;
using KuSys.DataAccess.Repositories.User;
using KuSys.Entities;
using KuSys.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KuSys.Services;

/// <summary>
/// User service to manage user operations.
/// </summary>
public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<User> _userManager;
    private readonly IStudentService _studentService;

    /// <summary>
    /// Constructor. Please use DependencyInjection.
    /// </summary>
    /// <param name="userRepository"></param>
    /// <param name="jwtSettings"></param>
    /// <param name="userManager"></param>
    /// <param name="studentService"></param>
    public UserService(IUserRepository userRepository,IOptions<JwtSettings> jwtSettings, UserManager<User> userManager,IStudentService studentService)
    {
        _userRepository = userRepository;
        _jwtSettings = jwtSettings.Value;
        _userManager = userManager;
        _studentService = studentService;
    }
    /// <summary>
    /// WithData new user with provided values.
    /// </summary>
    /// <param name="model">New user information</param>
    /// <returns><see cref="DbCreateResult{T}"/></returns>
    public async Task<RegisterResponseModel> CreateUser(RegisterRequestModel model)
    {
        var userEntity = model.Adapt<User>();
        // WithData user via userRepository.
        var result = await _userRepository.AddUser(userEntity,model.Password);

        if (result.Result == OperationResult.Success)
            return RegisterResponseModel.Success(result.Data);
        
        // return data
        return RegisterResponseModel.Fail();
    }
    /// <summary>
    /// Login using email and password
    /// </summary>
    /// <param name="requestModel">Login information</param>
    /// <returns><see cref="LoginResponseModel"/></returns>
    public async Task<LoginResponseModel> Login(LoginRequestModel requestModel)
    {
        // Find user with given email
        var foundUser = await _userManager.FindByEmailAsync(requestModel.Email);
        
        // if user was not found, return Error response.
        if (foundUser is null)
            throw new AuthenticationException($"User with email ({requestModel.Email}) was not found!");
        
        // Check if user password is correct
        var passwordMatch = await _userManager.CheckPasswordAsync(foundUser, requestModel.Password);
        
        // if provided passwor doesn't match, return Error response.
        if (passwordMatch is false)
            throw new AuthenticationException($"Provided password was not correct for user ({requestModel.Email})");
        
        // If provided login information was correct, generate JWT token with required claims.
        
        // Token Generation START
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
        var secret = new SymmetricSecurityKey(key);
        var creds= new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        var claims = await this.GetClaims(foundUser);
        var tokenOptions = GenerateTokenOptions(creds, claims);
        string token= new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        // Token Generation END
        
        
        // Return Login Response
        return new LoginResponseModel()
        {
            Token = token,
            IsSuccess = true
        };
    }

    /// <summary>
    /// Assing a user Admin Role (Test purposes Only)
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    public async Task<bool> MakeAdmin(LoginRequestModel requestModel)
    {
        // find user with given email
        var foundUser = await _userManager.FindByEmailAsync(requestModel.Email);
        
        // if user was not found, return false
        if (foundUser is null)
            return false;
        
        // Add user to Admin Role
        var addToRoleResult= await _userManager.AddToRoleAsync(foundUser, DefaultRoles.Admin);
        
        // If we couldn't add user into Admin role and user isn't already in the Admin role, return false
        if (!addToRoleResult.Succeeded && addToRoleResult.Errors.All(x => x.Code != "UserAlreadyInRole"))
            return false;
        
        // // Add required claims for admin role to user.
        // var addClaimsResult =  await _userManager.AddClaimsAsync(foundUser, DefaultRoles.AdminClaims());
        
        // If we werent able to add claims, remove user from the role and return false,
        // if (!addClaimsResult.Succeeded)
        // {
        //     await _userManager.RemoveFromRoleAsync(foundUser, DefaultRoles.Admin);
        //     return false;
        // }

        
        // If code reaches to that point, return true
        return true;
    }

    /// <summary>
    /// Get claims for a user to add into Token
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private async Task<IList<Claim>> GetClaims(User user)
    {
        var student =await _studentService.GetByUserId(user.Id);
        var claims = await _userManager.GetClaimsAsync(user);
        claims.Add(new Claim(ClaimTypes.NameIdentifier,student.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
        claims.Add(new Claim(ClaimTypes.Email, user.Email));
        claims.Add(new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString(CultureInfo.InvariantCulture)));
                
        var roles = await _userManager.GetRolesAsync(user);
        if(roles.Count>0)
            claims.Add(new Claim(ClaimTypes.Role,roles.First()));
        return claims;
    }
    
    /// <summary>
    /// Generate JWT Token with provided values.
    /// </summary>
    /// <param name="signingCredentials">Credentials to use</param>
    /// <param name="claims">Claims to add</param>
    /// <returns></returns>
    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IList<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken
        (
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.ExpiresIn)),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
   
}