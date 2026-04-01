using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public interface IFarmaceuticoRepository
{
    Task<Farmaceutico?> GetByIdAsync(int id);
    Task<IEnumerable<Farmaceutico>> GetAllAsync();
    Task<Farmaceutico> CreateAsync(Farmaceutico entity);
    Task<Farmaceutico> UpdateAsync(Farmaceutico entity);
    Task DeleteAsync(int id);
}
