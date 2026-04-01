using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniHealth.API.Models;

public enum StatusConsulta
{
    Agendada,
    Realizada,
    Cancelada
}

[Table("CONSULTA")]
public class Consulta
{
    [Key]
    [Column("id_consulta")]
    public int IdConsulta { get; set; }

    [ForeignKey(nameof(Prontuario))]
    [Column("id_prontuario")]
    public int IdProntuario { get; set; }

    /// <summary>
    /// Desnormalizado por performance — deve ser consistente com Prontuario.IdPaciente.
    /// </summary>
    [ForeignKey(nameof(Paciente))]
    [Column("id_paciente")]
    public int IdPaciente { get; set; }

    [ForeignKey(nameof(Medico))]
    [Column("id_medico")]
    public int IdMedico { get; set; }

    [Column("data_hora")]
    public DateTime DataHora { get; set; }

    [Column("status")]
    public StatusConsulta Status { get; set; } = StatusConsulta.Agendada;

    [MaxLength(2000)]
    [Column("diagnostico")]
    public string? Diagnostico { get; set; }

    [MaxLength(2000)]
    [Column("observacoes")]
    public string? Observacoes { get; set; }

    // Navegação
    public Prontuario           Prontuario  { get; set; } = null!;
    public Paciente             Paciente    { get; set; } = null!;
    public Medico               Medico      { get; set; } = null!;
    public ICollection<Prescricao> Prescricoes { get; set; } = [];
    public ICollection<Exame>     Exames     { get; set; } = [];
}
