using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public interface IPacienteRepository
{
    Task<Paciente?> GetByIdAsync(int id);
    Task<IEnumerable<Paciente>> GetAllAsync();
    Task<Paciente> CreateAsync(Paciente entity);
    Task<Paciente> UpdateAsync(Paciente entity);
    Task DeleteAsync(int id);
}
