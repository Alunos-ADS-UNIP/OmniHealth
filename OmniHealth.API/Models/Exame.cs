using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniHealth.API.Models;

public enum TipoExame
{
    Laboratorial,
    Imagem,
    Funcional,
    Outro
}

[Table("EXAME")]
public class Exame
{
    [Key]
    [Column("id_exame")]
    public int IdExame { get; set; }

    [ForeignKey(nameof(Prontuario))]
    [Column("id_prontuario")]
    public int IdProntuario { get; set; }

    [ForeignKey(nameof(Medico))]
    [Column("id_medico_solicitante")]
    public int IdMedicoSolicitante { get; set; }

    /// <summary>
    /// Opcional — pelo menos um entre id_consulta e id_internacao deve ser informado.
    /// Validação feita na camada de serviço (ExameService).
    /// </summary>
    [ForeignKey(nameof(Consulta))]
    [Column("id_consulta")]
    public int? IdConsulta { get; set; }

    /// <summary>
    /// Opcional — pelo menos um entre id_consulta e id_internacao deve ser informado.
    /// </summary>
    [ForeignKey(nameof(Internacao))]
    [Column("id_internacao")]
    public int? IdInternacao { get; set; }

    [Column("tipo")]
    public TipoExame Tipo { get; set; }

    [Required, MaxLength(500)]
    [Column("justificativa")]
    public string Justificativa { get; set; } = "";

    [Column("data_solicitacao")]
    public DateTime DataSolicitacao { get; set; } = DateTime.UtcNow;

    [Column("data_realizacao")]
    public DateTime? DataRealizacao { get; set; }

    [MaxLength(2000)]
    [Column("resultado")]
    public string? Resultado { get; set; }

    // Navegação
    public Prontuario  Prontuario  { get; set; } = null!;
    public Medico      Medico      { get; set; } = null!;
    public Consulta?   Consulta    { get; set; }
    public Internacao? Internacao  { get; set; }
}
