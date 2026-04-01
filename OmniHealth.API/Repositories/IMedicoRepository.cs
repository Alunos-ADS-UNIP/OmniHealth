using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public interface IMedicoRepository
{
    Task<Medico?> GetByIdAsync(int id);
    Task<IEnumerable<Medico>> GetAllAsync();
    Task<Medico> CreateAsync(Medico entity);
    Task<Medico> UpdateAsync(Medico entity);
    Task DeleteAsync(int id);
}
