using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public interface IMedicamentoRepository
{
    Task<Medicamento?> GetByIdAsync(int id);
    Task<IEnumerable<Medicamento>> GetAllAsync();
    Task<Medicamento> CreateAsync(Medicamento entity);
    Task<Medicamento> UpdateAsync(Medicamento entity);
    Task DeleteAsync(int id);
}
