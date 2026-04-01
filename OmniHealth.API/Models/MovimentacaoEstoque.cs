using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniHealth.API.Models;

public enum TipoMovimentacao
{
    Entrada,
    Saida
}

[Table("MOVIMENTACAO_ESTOQUE")]
public class MovimentacaoEstoque
{
    [Key]
    [Column("id_movimentacao")]
    public int IdMovimentacao { get; set; }

    [ForeignKey(nameof(Lote))]
    [Column("id_lote")]
    public int IdLote { get; set; }

    /// <summary>
    /// Preenchido apenas em saídas (dispensações). Rastreabilidade por item de prescrição.
    /// </summary>
    [ForeignKey(nameof(ItemPrescricao))]
    [Column("id_item")]
    public int? IdItem { get; set; }

    [ForeignKey(nameof(Farmaceutico))]
    [Column("id_farmaceutico")]
    public int IdFarmaceutico { get; set; }

    [Column("tipo")]
    public TipoMovimentacao Tipo { get; set; }

    [Column("quantidade")]
    public int Quantidade { get; set; }

    [Column("data_movimentacao")]
    public DateTime DataMovimentacao { get; set; } = DateTime.UtcNow;

    [MaxLength(500)]
    [Column("observacao")]
    public string? Observacao { get; set; }

    // Navegação
    public Lote            Lote           { get; set; } = null!;
    public ItemPrescricao? ItemPrescricao { get; set; }
    public Farmaceutico    Farmaceutico   { get; set; } = null!;
}
