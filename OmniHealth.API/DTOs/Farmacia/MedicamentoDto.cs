using System.ComponentModel.DataAnnotations;

namespace OmniHealth.API.DTOs.Farmacia;

public class MedicamentoDto
{
    [Required, MaxLength(200)]
    public string Nome { get; set; } = "";

    [MaxLength(200)]
    public string? PrincipioAtivo { get; set; }

    [MaxLength(100)]
    public string? Tipo { get; set; }

    [MaxLength(500)]
    public string? Descricao { get; set; }

    public int EstoqueMinimo { get; set; } = 0;
}

public class MedicamentoResponseDto : MedicamentoDto
{
    public int IdMedicamento   { get; set; }
    public int EstoqueAtual    { get; set; }
    public bool AlertaMinimo   { get; set; }
    public bool Ativo          { get; set; }
}
