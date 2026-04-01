using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public interface IInternacaoRepository
{
    Task<Internacao?> GetByIdAsync(int id);
    Task<IEnumerable<Internacao>> GetAllAsync();
    Task<Internacao> CreateAsync(Internacao entity);
    Task<Internacao> UpdateAsync(Internacao entity);
    Task DeleteAsync(int id);
}
