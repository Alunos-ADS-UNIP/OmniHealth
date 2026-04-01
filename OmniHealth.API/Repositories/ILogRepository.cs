using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public interface ILogRepository
{
    Task<Log?> GetByIdAsync(int id);
    Task<IEnumerable<Log>> GetAllAsync();
    Task<Log> CreateAsync(Log entity);
    Task<Log> UpdateAsync(Log entity);
    Task DeleteAsync(int id);
}
