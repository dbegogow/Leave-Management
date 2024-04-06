namespace LeaveManagement.Api.Controllers;

using LeaveManagement.Application.Contracts.Identity;
using LeaveManagement.Application.Models.Identity;

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService authenticationService;

    public AuthController(IAuthService authenticationService)
        => this.authenticationService = authenticationService;

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        => Ok(await this.authenticationService.Login(request));

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        => Ok(await this.authenticationService.Register(request));
}
