using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public interface ILoteRepository
{
    Task<Lote?> GetByIdAsync(int id);
    Task<IEnumerable<Lote>> GetAllAsync();
    Task<Lote> CreateAsync(Lote entity);
    Task<Lote> UpdateAsync(Lote entity);
    Task DeleteAsync(int id);
}
