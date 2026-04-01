namespace OmniHealth.API.Helpers;

/// <summary>
/// Wrapper sobre BCrypt.Net para hashing e verificação de senhas.
/// Fator de custo ≥ 12 conforme especificação (OWASP).
/// </summary>
public static class PasswordHelper
{
    private const int WorkFactor = 12;

    public static string Hash(string plainPassword)
        => BCrypt.Net.BCrypt.HashPassword(plainPassword, WorkFactor);

    public static bool Verify(string plainPassword, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
}
