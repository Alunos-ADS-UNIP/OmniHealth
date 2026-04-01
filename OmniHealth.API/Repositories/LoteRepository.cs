using Microsoft.EntityFrameworkCore;
using OmniHealth.API.Data;
using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public class LoteRepository : ILoteRepository
{
    protected readonly OmniHealthDbContext _db;
    public LoteRepository(OmniHealthDbContext db) => _db = db;

    public async Task<Lote?> GetByIdAsync(int id)
        => await _db.Set<Lote>().FindAsync(id);

    public async Task<IEnumerable<Lote>> GetAllAsync()
        => await _db.Set<Lote>().ToListAsync();

    public async Task<Lote> CreateAsync(Lote entity)
    {
        _db.Set<Lote>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Lote> UpdateAsync(Lote entity)
    {
        _db.Set<Lote>().Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Lote com id {id} não encontrado.");
        _db.Set<Lote>().Remove(entity);
        await _db.SaveChangesAsync();
    }
}
