using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniHealth.API.Models;

public enum TipoUsuario
{
    MED,
    PAC,
    FUNC,
    FARM
}

[Table("USUARIO")]
public class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Required, MaxLength(150)]
    [Column("nome")]
    public string Nome { get; set; } = "";

    [Required, MaxLength(150)]
    [Column("email")]
    public string Email { get; set; } = "";

    [Required, StringLength(11)]
    [Column("cpf")]
    public string Cpf { get; set; } = "";

    [Required, MaxLength(255)]
    [Column("senha")]
    public string Senha { get; set; } = "";

    [Column("tipo_usuario")]
    public TipoUsuario TipoUsuario { get; set; }

    [Column("ativo")]
    public bool Ativo { get; set; } = true;

    [Column("data_cadastro")]
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    [Column("tentativas_login")]
    public int TentativasLogin { get; set; } = 0;

    [Column("bloqueado_ate")]
    public DateTime? BloqueadoAte { get; set; }

    // Navegação
    public Paciente?      Paciente      { get; set; }
    public Medico?        Medico        { get; set; }
    public Funcionario?   Funcionario   { get; set; }
    public Farmaceutico?  Farmaceutico  { get; set; }
    public ICollection<Log> Logs        { get; set; } = [];
}
