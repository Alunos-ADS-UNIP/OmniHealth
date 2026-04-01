using Microsoft.EntityFrameworkCore;
using OmniHealth.API.Data;
using Xunit;

namespace OmniHealth.Tests.Services;

/// <summary>
/// Testes unitários para AuthService.
/// Usa banco de dados em memória (EF InMemory).
/// </summary>
public class AuthServiceTests
{
    private OmniHealthDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<OmniHealthDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new OmniHealthDbContext(options);
    }

    [Fact]
    public void PlaceholderTest_ShouldPass()
    {
        // TODO: Implementar testes reais quando AuthService estiver completo
        Assert.True(true);
    }
}
