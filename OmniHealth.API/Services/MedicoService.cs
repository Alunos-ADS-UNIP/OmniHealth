using OmniHealth.API.Data;

namespace OmniHealth.API.Services;

public class MedicoService : IMedicoService
{
    private readonly OmniHealthDbContext _db;

    public MedicoService(OmniHealthDbContext db)
    {
        _db = db;
    }

    // TODO: Implementar métodos
}
