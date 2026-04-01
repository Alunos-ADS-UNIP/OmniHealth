using System.ComponentModel.DataAnnotations;

namespace OmniHealth.API.DTOs.Auth;

/// <summary>
/// DTO para auto-cadastro de pacientes (RF02).
/// </summary>
public class CadastroRequestDto
{
    [Required, MaxLength(150)]
    public string Nome { get; set; } = "";

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = "";

    [Required, StringLength(11, MinimumLength = 11)]
    public string Cpf { get; set; } = "";

    [Required, MinLength(8)]
    public string Senha { get; set; } = "";

    [MaxLength(20)]
    public string? Telefone { get; set; }

    public DateOnly? DataNascimento { get; set; }

    [MaxLength(10)]
    public string? Sexo { get; set; }

    [MaxLength(255)]
    public string? Endereco { get; set; }
}
