using OmniHealth.API.Data;

namespace OmniHealth.API.Services;

public class AuthService : IAuthService
{
    private readonly OmniHealthDbContext _db;

    public AuthService(OmniHealthDbContext db)
    {
        _db = db;
    }

    // TODO: Implementar métodos
}
