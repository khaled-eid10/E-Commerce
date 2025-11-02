using Microsoft.AspNetCore.Authorization;
using Shared.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Presentation.Controllers;
public class AuthenticationController(IServiceManager serviceManager)
    : APIController
{

    // Login (email , password) => {email , DisplayName  , token }
    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> Login(LoginRequest request)
        => Ok(await serviceManager.AuthenticationService.LoginAsync(request));
    // Register (email  ,password , uN ,dN , pN ) => {email , DisplayName  , token }
    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register(RegisterRequest request)
    => Ok(await serviceManager.AuthenticationService.RegisterAsync(request));
    // CheckEmail(string email) => bool;
    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmail([EmailAddress] string email)
        => Ok(await serviceManager.AuthenticationService.CheckEmailAsync(email));


    // [Authorize] GetCurrentUser => {email , DisplayName  , token }
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserResponse>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return Ok(await serviceManager.AuthenticationService.GetUserByEmailAsync(email!));
    }
    // [Authorize] Get User Address => AddressDTO 

    [Authorize]
    [HttpGet("Address")]
    public async Task<ActionResult<AddressDTO>> GetAddress()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return Ok(await serviceManager.AuthenticationService.GetAddressAsync(email!));
    }
    // [Authorize] Update User Address => AddressDTO 

    [Authorize]
    [HttpPut("Address")]
    public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO addressDTO)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return Ok(await serviceManager.AuthenticationService.UpdateAddressAsync(addressDTO, email!));
    }

}
