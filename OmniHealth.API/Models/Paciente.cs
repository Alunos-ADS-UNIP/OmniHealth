using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniHealth.API.Models;

[Table("PACIENTE")]
public class Paciente
{
    [Key]
    [Column("id_paciente")]
    public int IdPaciente { get; set; }

    [ForeignKey(nameof(Usuario))]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [MaxLength(20)]
    [Column("telefone")]
    public string? Telefone { get; set; }

    [Column("data_nascimento")]
    public DateOnly? DataNascimento { get; set; }

    [MaxLength(10)]
    [Column("sexo")]
    public string? Sexo { get; set; }

    [MaxLength(255)]
    [Column("endereco")]
    public string? Endereco { get; set; }

    // Navegação
    public Usuario   Usuario    { get; set; } = null!;
    public Prontuario? Prontuario { get; set; }
}
