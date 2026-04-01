using Microsoft.EntityFrameworkCore;
using OmniHealth.API.Data;
using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public class LogRepository : ILogRepository
{
    protected readonly OmniHealthDbContext _db;
    public LogRepository(OmniHealthDbContext db) => _db = db;

    public async Task<Log?> GetByIdAsync(int id)
        => await _db.Set<Log>().FindAsync(id);

    public async Task<IEnumerable<Log>> GetAllAsync()
        => await _db.Set<Log>().ToListAsync();

    public async Task<Log> CreateAsync(Log entity)
    {
        _db.Set<Log>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Log> UpdateAsync(Log entity)
    {
        _db.Set<Log>().Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Log com id {id} não encontrado.");
        _db.Set<Log>().Remove(entity);
        await _db.SaveChangesAsync();
    }
}
