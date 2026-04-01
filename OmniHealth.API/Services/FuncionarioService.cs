using OmniHealth.API.Data;

namespace OmniHealth.API.Services;

public class FuncionarioService : IFuncionarioService
{
    private readonly OmniHealthDbContext _db;

    public FuncionarioService(OmniHealthDbContext db)
    {
        _db = db;
    }

    // TODO: Implementar métodos
}
