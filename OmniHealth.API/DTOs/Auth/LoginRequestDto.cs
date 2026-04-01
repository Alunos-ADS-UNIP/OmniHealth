using System.ComponentModel.DataAnnotations;

namespace OmniHealth.API.DTOs.Auth;

public class LoginRequestDto
{
    [Required, StringLength(11, MinimumLength = 11)]
    public string Cpf { get; set; } = "";

    [Required]
    public string Senha { get; set; } = "";
}
