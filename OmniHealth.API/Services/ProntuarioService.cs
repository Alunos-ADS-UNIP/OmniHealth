using OmniHealth.API.Data;

namespace OmniHealth.API.Services;

public class ProntuarioService : IProntuarioService
{
    private readonly OmniHealthDbContext _db;

    public ProntuarioService(OmniHealthDbContext db)
    {
        _db = db;
    }

    // TODO: Implementar métodos
}
