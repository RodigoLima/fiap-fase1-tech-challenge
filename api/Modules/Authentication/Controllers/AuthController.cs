﻿using fiap_fase1_tech_challenge.Modules.Authentication.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Authentication.Services.Interfaces;
using fiap_fase1_tech_challenge.Modules.Users.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
  }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userService.AuthenticateAsync(request.Email, request.Password);

        var tokens = _authService.GenerateTokens(user!.Id.ToString(), user.Email, user.Role.Name);
        return Ok(tokens);
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public IActionResult Refresh([FromBody] TokenRequest request)
    {
        var principal = _authService.GetPrincipalFromExpiredToken(request.RefreshToken);
        if (principal is null)
            return Unauthorized("Refresh token inválido");

        var tokenType = principal.FindFirstValue("token_type");
        if (tokenType != "refresh")
            return Unauthorized("Este não é um refresh token válido");

        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = principal.FindFirstValue(ClaimTypes.Email);
        var role = principal.FindFirstValue(ClaimTypes.Role);

        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(role))
            return Unauthorized("Token inválido: informações obrigatórias ausentes");

        var tokens = _authService.GenerateTokens(userId, email, role);
        return Ok(tokens);
    }
}
