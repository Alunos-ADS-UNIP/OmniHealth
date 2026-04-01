using OmniHealth.API.Data;

namespace OmniHealth.API.Services;

public class RelatorioService : IRelatorioService
{
    private readonly OmniHealthDbContext _db;

    public RelatorioService(OmniHealthDbContext db)
    {
        _db = db;
    }

    // TODO: Implementar métodos
}
