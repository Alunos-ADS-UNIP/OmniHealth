using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public interface IProntuarioRepository
{
    Task<Prontuario?> GetByIdAsync(int id);
    Task<IEnumerable<Prontuario>> GetAllAsync();
    Task<Prontuario> CreateAsync(Prontuario entity);
    Task<Prontuario> UpdateAsync(Prontuario entity);
    Task DeleteAsync(int id);
}
