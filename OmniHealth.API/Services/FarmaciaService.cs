using OmniHealth.API.Data;

namespace OmniHealth.API.Services;

public class FarmaciaService : IFarmaciaService
{
    private readonly OmniHealthDbContext _db;

    public FarmaciaService(OmniHealthDbContext db)
    {
        _db = db;
    }

    // TODO: Implementar métodos
}
