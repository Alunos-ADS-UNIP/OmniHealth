using Microsoft.AspNetCore.Mvc;
using OmniHealth.API.DTOs.Auth;
using OmniHealth.API.Services;

namespace OmniHealth.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>POST /api/auth/login — Login com CPF e senha (RF01)</summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        // TODO: Implementar em AuthService
        throw new NotImplementedException();
    }

    /// <summary>POST /api/auth/cadastro — Auto-cadastro de paciente (RF02)</summary>
    [HttpPost("cadastro")]
    public async Task<IActionResult> Cadastro([FromBody] CadastroRequestDto dto)
    {
        // TODO: Implementar em AuthService
        throw new NotImplementedException();
    }

    /// <summary>POST /api/auth/recuperar-senha — Recuperação de senha por e-mail (RF04)</summary>
    [HttpPost("recuperar-senha")]
    public async Task<IActionResult> RecuperarSenha([FromBody] string email)
    {
        // TODO: Implementar em AuthService
        throw new NotImplementedException();
    }
}
