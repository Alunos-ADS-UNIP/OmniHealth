using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniHealth.API.Models;

[Table("PRESCRICAO")]
public class Prescricao
{
    [Key]
    [Column("id_prescricao")]
    public int IdPrescricao { get; set; }

    [ForeignKey(nameof(Consulta))]
    [Column("id_consulta")]
    public int IdConsulta { get; set; }

    [Column("data_prescricao")]
    public DateTime DataPrescricao { get; set; } = DateTime.UtcNow;

    [Column("ativa")]
    public bool Ativa { get; set; } = true;

    [MaxLength(500)]
    [Column("observacoes")]
    public string? Observacoes { get; set; }

    // Navegação
    public Consulta                  Consulta { get; set; } = null!;
    public ICollection<ItemPrescricao> Itens   { get; set; } = [];
}
