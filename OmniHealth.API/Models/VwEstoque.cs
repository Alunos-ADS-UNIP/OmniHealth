using System.ComponentModel.DataAnnotations.Schema;

namespace OmniHealth.API.Models;

/// <summary>
/// Entidade sem chave mapeada para a view VW_ESTOQUE (somente leitura).
/// Representa o estoque disponível por medicamento, calculado via soma dos lotes com validade vigente.
/// </summary>
[Table("VW_ESTOQUE")]
public class VwEstoque
{
    [Column("id_medicamento")]
    public int IdMedicamento { get; set; }

    [Column("quantidade_total")]
    public int QuantidadeTotal { get; set; }
}
