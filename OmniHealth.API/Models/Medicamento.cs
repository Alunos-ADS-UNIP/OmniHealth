using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniHealth.API.Models;

[Table("MEDICAMENTO")]
public class Medicamento
{
    [Key]
    [Column("id_medicamento")]
    public int IdMedicamento { get; set; }

    [Required, MaxLength(200)]
    [Column("nome")]
    public string Nome { get; set; } = "";

    [MaxLength(200)]
    [Column("principio_ativo")]
    public string? PrincipioAtivo { get; set; }

    [MaxLength(100)]
    [Column("tipo")]
    public string? Tipo { get; set; }

    [MaxLength(500)]
    [Column("descricao")]
    public string? Descricao { get; set; }

    [Column("estoque_minimo")]
    public int EstoqueMinimo { get; set; } = 0;

    [Column("ativo")]
    public bool Ativo { get; set; } = true;

    // Navegação
    public ICollection<Lote>           Lotes   { get; set; } = [];
    public ICollection<ItemPrescricao> Itens   { get; set; } = [];
}
