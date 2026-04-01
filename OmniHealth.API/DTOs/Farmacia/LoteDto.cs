using System.ComponentModel.DataAnnotations;

namespace OmniHealth.API.DTOs.Farmacia;

public class LoteDto
{
    [Required]
    public int IdMedicamento { get; set; }

    [Required, MaxLength(100)]
    public string NumeroLote { get; set; } = "";

    [Required]
    public DateOnly DataValidade { get; set; }

    [MaxLength(200)]
    public string? Fornecedor { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantidade { get; set; }
}

public class LoteResponseDto : LoteDto
{
    public int      IdLote      { get; set; }
    public DateTime DataEntrada { get; set; }
    public bool     Vencido     { get; set; }
    public bool     ProximoVencimento { get; set; }
}
