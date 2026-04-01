using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniHealth.API.Models;

[Table("FUNCIONARIO")]
public class Funcionario
{
    [Key]
    [Column("id_funcionario")]
    public int IdFuncionario { get; set; }

    [ForeignKey(nameof(Usuario))]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Required, MaxLength(100)]
    [Column("cargo")]
    public string Cargo { get; set; } = "";

    [Required, MaxLength(100)]
    [Column("setor")]
    public string Setor { get; set; } = "";

    // Navegação
    public Usuario              Usuario    { get; set; } = null!;
    public ICollection<Internacao> Internacoes { get; set; } = [];
}
