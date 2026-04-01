namespace OmniHealth.API.DTOs.Auth;

public class LoginResponseDto
{
    public string Token      { get; set; } = "";
    public string TipoUsuario { get; set; } = "";
    public int    IdUsuario  { get; set; }
    public string Nome       { get; set; } = "";
    public DateTime Expiracao { get; set; }
}
