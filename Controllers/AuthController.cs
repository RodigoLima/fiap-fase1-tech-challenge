using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace fiap_fase1_tech_challenge.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly AuthService _authService;

    public AuthController(IUserService userService, AuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userService.AuthenticateAsync(request.Email, request.Password);

        if (user is null)
            return Unauthorized("Credenciais inválidas");

        var token = _authService.GenerateToken(user.Id.ToString(), user.Email, "Admin");
        return Ok(new { token });
    }
}
