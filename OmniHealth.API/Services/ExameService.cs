using OmniHealth.API.Data;

namespace OmniHealth.API.Services;

public class ExameService : IExameService
{
    private readonly OmniHealthDbContext _db;

    public ExameService(OmniHealthDbContext db)
    {
        _db = db;
    }

    // TODO: Implementar métodos
}
