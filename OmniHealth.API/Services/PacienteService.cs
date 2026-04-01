using OmniHealth.API.Data;

namespace OmniHealth.API.Services;

public class PacienteService : IPacienteService
{
    private readonly OmniHealthDbContext _db;

    public PacienteService(OmniHealthDbContext db)
    {
        _db = db;
    }

    // TODO: Implementar métodos
}
