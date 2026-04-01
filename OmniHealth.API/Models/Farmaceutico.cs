using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniHealth.API.Models;

[Table("FARMACEUTICO")]
public class Farmaceutico
{
    [Key]
    [Column("id_farmaceutico")]
    public int IdFarmaceutico { get; set; }

    [ForeignKey(nameof(Usuario))]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Required, MaxLength(20)]
    [Column("crf")]
    public string Crf { get; set; } = "";

    // Navegação
    public Usuario                        Usuario      { get; set; } = null!;
    public ICollection<MovimentacaoEstoque> Movimentacoes { get; set; } = [];
}
