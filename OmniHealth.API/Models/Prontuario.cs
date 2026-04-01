using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniHealth.API.Models;

[Table("PRONTUARIO")]
public class Prontuario
{
    [Key]
    [Column("id_prontuario")]
    public int IdProntuario { get; set; }

    [ForeignKey(nameof(Paciente))]
    [Column("id_paciente")]
    public int IdPaciente { get; set; }

    [Column("data_abertura")]
    public DateTime DataAbertura { get; set; } = DateTime.UtcNow;

    [MaxLength(1000)]
    [Column("observacoes_gerais")]
    public string? ObservacoesGerais { get; set; }

    // Navegação
    public Paciente            Paciente   { get; set; } = null!;
    public ICollection<Consulta> Consultas { get; set; } = [];
    public ICollection<Exame>    Exames    { get; set; } = [];
    public ICollection<Internacao> Internacoes { get; set; } = [];
}
