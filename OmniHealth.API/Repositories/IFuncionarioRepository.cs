using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public interface IFuncionarioRepository
{
    Task<Funcionario?> GetByIdAsync(int id);
    Task<IEnumerable<Funcionario>> GetAllAsync();
    Task<Funcionario> CreateAsync(Funcionario entity);
    Task<Funcionario> UpdateAsync(Funcionario entity);
    Task DeleteAsync(int id);
}
