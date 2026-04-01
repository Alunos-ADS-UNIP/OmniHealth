using OmniHealth.API.Data;

namespace OmniHealth.API.Services;

public class InternacaoService : IInternacaoService
{
    private readonly OmniHealthDbContext _db;

    public InternacaoService(OmniHealthDbContext db)
    {
        _db = db;
    }

    // TODO: Implementar métodos
}
