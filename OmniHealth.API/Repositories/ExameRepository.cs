using Microsoft.EntityFrameworkCore;
using OmniHealth.API.Data;
using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public class ExameRepository : IExameRepository
{
    protected readonly OmniHealthDbContext _db;
    public ExameRepository(OmniHealthDbContext db) => _db = db;

    public async Task<Exame?> GetByIdAsync(int id)
        => await _db.Set<Exame>().FindAsync(id);

    public async Task<IEnumerable<Exame>> GetAllAsync()
        => await _db.Set<Exame>().ToListAsync();

    public async Task<Exame> CreateAsync(Exame entity)
    {
        _db.Set<Exame>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Exame> UpdateAsync(Exame entity)
    {
        _db.Set<Exame>().Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Exame com id {id} não encontrado.");
        _db.Set<Exame>().Remove(entity);
        await _db.SaveChangesAsync();
    }
}
