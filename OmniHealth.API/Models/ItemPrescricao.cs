using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniHealth.API.Models;

[Table("ITEM_PRESCRICAO")]
public class ItemPrescricao
{
    [Key]
    [Column("id_item")]
    public int IdItem { get; set; }

    [ForeignKey(nameof(Prescricao))]
    [Column("id_prescricao")]
    public int IdPrescricao { get; set; }

    [ForeignKey(nameof(Medicamento))]
    [Column("id_medicamento")]
    public int IdMedicamento { get; set; }

    [Required, MaxLength(200)]
    [Column("dosagem")]
    public string Dosagem { get; set; } = "";

    [Required, MaxLength(500)]
    [Column("posologia")]
    public string Posologia { get; set; } = "";

    [Column("quantidade_prescrita")]
    public int QuantidadePrescrita { get; set; }

    [Column("dispensado")]
    public bool Dispensado { get; set; } = false;

    // Navegação
    public Prescricao   Prescricao   { get; set; } = null!;
    public Medicamento  Medicamento  { get; set; } = null!;
    public ICollection<MovimentacaoEstoque> Movimentacoes { get; set; } = [];
}
