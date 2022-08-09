using DavidsBlog.Application.Services.Authentication;
using DavidsBlog.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DavidsBlog.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService applicationService;

    public AuthenticationController(IAuthenticationService applicationService)
    {
        this.applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = this.applicationService.Register(
            request.FirstName, 
            request.LastName, 
            request.Email, 
            request.Password);

        var response = new AuthenticationResponse(
            authResult.Id,
            authResult.FirstName,
            authResult.Lastname,
            authResult.Email,
            authResult.Token);

        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = this.applicationService.Login(
            request.Email,
            request.Password);

        var response = new AuthenticationResponse(
            authResult.Id,
            authResult.FirstName,
            authResult.Lastname,
            authResult.Email,
            authResult.Token);

        return Ok(response);
    }
}