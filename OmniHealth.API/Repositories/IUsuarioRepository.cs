using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(int id);
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario> CreateAsync(Usuario entity);
    Task<Usuario> UpdateAsync(Usuario entity);
    Task DeleteAsync(int id);
}
