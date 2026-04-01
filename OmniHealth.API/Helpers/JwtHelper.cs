using Microsoft.IdentityModel.Tokens;
using OmniHealth.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OmniHealth.API.Helpers;

public class JwtHelper
{
    private readonly IConfiguration _config;

    public JwtHelper(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(Usuario usuario)
    {
        var key     = _config["Jwt:Key"]!;
        var issuer  = _config["Jwt:Issuer"]!;
        var audience = _config["Jwt:Audience"]!;
        var hours   = int.Parse(_config["Jwt:ExpirationHours"] ?? "8");

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
            new(ClaimTypes.Name,           usuario.Nome),
            new(ClaimTypes.Email,          usuario.Email),
            new(ClaimTypes.Role,           usuario.TipoUsuario.ToString()),
            new("cpf",                     usuario.Cpf)
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer:             issuer,
            audience:           audience,
            claims:             claims,
            expires:            DateTime.UtcNow.AddHours(hours),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public DateTime GetExpiration()
    {
        var hours = int.Parse(_config["Jwt:ExpirationHours"] ?? "8");
        return DateTime.UtcNow.AddHours(hours);
    }
}
