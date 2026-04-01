using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniHealth.API.Models;

[Table("MEDICO")]
public class Medico
{
    [Key]
    [Column("id_medico")]
    public int IdMedico { get; set; }

    [ForeignKey(nameof(Usuario))]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Required, MaxLength(20)]
    [Column("crm")]
    public string Crm { get; set; } = "";

    [Required, MaxLength(100)]
    [Column("especialidade")]
    public string Especialidade { get; set; } = "";

    // Navegação
    public Usuario              Usuario    { get; set; } = null!;
    public ICollection<Consulta>  Consultas  { get; set; } = [];
    public ICollection<Exame>     Exames     { get; set; } = [];
    public ICollection<Internacao> Internacoes { get; set; } = [];
}
