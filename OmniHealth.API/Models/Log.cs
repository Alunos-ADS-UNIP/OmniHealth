using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniHealth.API.Models;

[Table("LOG")]
public class Log
{
    [Key]
    [Column("id_log")]
    public int IdLog { get; set; }

    [ForeignKey(nameof(Usuario))]
    [Column("id_usuario")]
    public int? IdUsuario { get; set; }

    [Required, MaxLength(100)]
    [Column("acao")]
    public string Acao { get; set; } = "";

    [MaxLength(500)]
    [Column("descricao")]
    public string? Descricao { get; set; }

    [MaxLength(45)]
    [Column("ip")]
    public string? Ip { get; set; }

    [Column("data_hora")]
    public DateTime DataHora { get; set; } = DateTime.UtcNow;

    // Navegação
    public Usuario? Usuario { get; set; }
}
