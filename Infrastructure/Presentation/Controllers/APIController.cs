global using Microsoft.AspNetCore.Mvc;
global using ServicesAbstractions;
global using Shared;
global using Shared.Products;
using System.Security.Claims;
namespace Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public abstract class APIController : ControllerBase
{
    protected string GetEmailFromToken() => User.FindFirstValue(ClaimTypes.Email)!;
}
