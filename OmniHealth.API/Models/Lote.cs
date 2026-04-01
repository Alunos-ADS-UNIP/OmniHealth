using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniHealth.API.Models;

[Table("LOTE")]
public class Lote
{
    [Key]
    [Column("id_lote")]
    public int IdLote { get; set; }

    [ForeignKey(nameof(Medicamento))]
    [Column("id_medicamento")]
    public int IdMedicamento { get; set; }

    [Required, MaxLength(100)]
    [Column("numero_lote")]
    public string NumeroLote { get; set; } = "";

    [Column("data_validade")]
    public DateOnly DataValidade { get; set; }

    [MaxLength(200)]
    [Column("fornecedor")]
    public string? Fornecedor { get; set; }

    [Column("quantidade")]
    public int Quantidade { get; set; }

    [Column("data_entrada")]
    public DateTime DataEntrada { get; set; } = DateTime.UtcNow;

    // Navegação
    public Medicamento Medicamento { get; set; } = null!;
    public ICollection<MovimentacaoEstoque> Movimentacoes { get; set; } = [];
}
