using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public interface IPrescricaoRepository
{
    Task<Prescricao?> GetByIdAsync(int id);
    Task<IEnumerable<Prescricao>> GetAllAsync();
    Task<Prescricao> CreateAsync(Prescricao entity);
    Task<Prescricao> UpdateAsync(Prescricao entity);
    Task DeleteAsync(int id);
}
