using System.ComponentModel.DataAnnotations;
using OmniHealth.API.Models;

namespace OmniHealth.API.DTOs.Consulta;

public class AgendarConsultaDto
{
    [Required]
    public int IdProntuario { get; set; }

    [Required]
    public int IdMedico { get; set; }

    [Required]
    public DateTime DataHora { get; set; }
}

public class RegistrarDiagnosticoDto
{
    [MaxLength(2000)]
    public string? Diagnostico { get; set; }

    [MaxLength(2000)]
    public string? Observacoes { get; set; }
}

public class ConsultaResponseDto
{
    public int            IdConsulta   { get; set; }
    public int            IdProntuario { get; set; }
    public int            IdPaciente   { get; set; }
    public string         NomePaciente { get; set; } = "";
    public int            IdMedico     { get; set; }
    public string         NomeMedico   { get; set; } = "";
    public DateTime       DataHora     { get; set; }
    public StatusConsulta Status       { get; set; }
    public string?        Diagnostico  { get; set; }
    public string?        Observacoes  { get; set; }
}
