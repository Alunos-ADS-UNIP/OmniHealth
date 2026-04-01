using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public interface IConsultaRepository
{
    Task<Consulta?> GetByIdAsync(int id);
    Task<IEnumerable<Consulta>> GetAllAsync();
    Task<Consulta> CreateAsync(Consulta entity);
    Task<Consulta> UpdateAsync(Consulta entity);
    Task DeleteAsync(int id);
}
