using OmniHealth.API.Data;

namespace OmniHealth.API.Services;

public class ConsultaService : IConsultaService
{
    private readonly OmniHealthDbContext _db;

    public ConsultaService(OmniHealthDbContext db)
    {
        _db = db;
    }

    // TODO: Implementar métodos
}
