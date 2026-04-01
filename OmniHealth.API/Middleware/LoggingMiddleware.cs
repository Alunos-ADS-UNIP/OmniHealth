using OmniHealth.API.Data;
using OmniHealth.API.Models;
using System.Security.Claims;

namespace OmniHealth.API.Middleware;

/// <summary>
/// Middleware que registra na tabela LOG as requisições para endpoints críticos.
/// </summary>
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    // Prefixos de rota que devem ser registrados no log
    private static readonly string[] _rotasCriticas =
    [
        "/api/auth",
        "/api/prescricoes",
        "/api/farmacia/dispensar",
        "/api/internacoes",
        "/api/exames"
    ];

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, OmniHealthDbContext db)
    {
        await _next(context);

        // Registra apenas métodos de escrita em rotas críticas
        var metodo = context.Request.Method;
        var path   = context.Request.Path.Value ?? "";

        bool ehCritica = _rotasCriticas.Any(r => path.StartsWith(r, StringComparison.OrdinalIgnoreCase));
        bool ehEscrita = metodo is "POST" or "PUT" or "PATCH" or "DELETE";

        if (ehCritica && ehEscrita && context.Response.StatusCode < 400)
        {
            var idUsuarioStr = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int? idUsuario   = int.TryParse(idUsuarioStr, out var id) ? id : null;

            var log = new Log
            {
                IdUsuario = idUsuario,
                Acao      = $"{metodo} {path}",
                Descricao = $"Status: {context.Response.StatusCode}",
                Ip        = context.Connection.RemoteIpAddress?.ToString(),
                DataHora  = DateTime.UtcNow
            };

            db.Logs.Add(log);
            await db.SaveChangesAsync();
        }
    }
}
