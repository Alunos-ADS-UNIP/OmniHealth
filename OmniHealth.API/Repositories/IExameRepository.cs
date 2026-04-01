using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public interface IExameRepository
{
    Task<Exame?> GetByIdAsync(int id);
    Task<IEnumerable<Exame>> GetAllAsync();
    Task<Exame> CreateAsync(Exame entity);
    Task<Exame> UpdateAsync(Exame entity);
    Task DeleteAsync(int id);
}
