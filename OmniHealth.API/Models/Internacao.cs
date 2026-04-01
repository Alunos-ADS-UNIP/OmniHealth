using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniHealth.API.Models;

public enum StatusInternacao
{
    Ativa,
    Alta,
    Transferida
}

[Table("INTERNACAO")]
public class Internacao
{
    [Key]
    [Column("id_internacao")]
    public int IdInternacao { get; set; }

    [ForeignKey(nameof(Prontuario))]
    [Column("id_prontuario")]
    public int IdProntuario { get; set; }

    [ForeignKey(nameof(Medico))]
    [Column("id_medico_responsavel")]
    public int IdMedicoResponsavel { get; set; }

    [ForeignKey(nameof(Funcionario))]
    [Column("id_funcionario")]
    public int IdFuncionario { get; set; }

    [Column("data_entrada")]
    public DateTime DataEntrada { get; set; } = DateTime.UtcNow;

    [Required, MaxLength(1000)]
    [Column("motivo")]
    public string Motivo { get; set; } = "";

    [MaxLength(50)]
    [Column("leito")]
    public string? Leito { get; set; }

    [Column("status")]
    public StatusInternacao Status { get; set; } = StatusInternacao.Ativa;

    [Column("data_alta")]
    public DateTime? DataAlta { get; set; }

    [MaxLength(500)]
    [Column("condicao_alta")]
    public string? CondicaoAlta { get; set; }

    // Navegação
    public Prontuario         Prontuario  { get; set; } = null!;
    public Medico             Medico      { get; set; } = null!;
    public Funcionario        Funcionario { get; set; } = null!;
    public ICollection<Exame> Exames      { get; set; } = [];
}
