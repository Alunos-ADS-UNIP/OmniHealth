using Microsoft.EntityFrameworkCore;
using OmniHealth.API.Data;
using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public class MedicoRepository : IMedicoRepository
{
    protected readonly OmniHealthDbContext _db;
    public MedicoRepository(OmniHealthDbContext db) => _db = db;

    public async Task<Medico?> GetByIdAsync(int id)
        => await _db.Set<Medico>().FindAsync(id);

    public async Task<IEnumerable<Medico>> GetAllAsync()
        => await _db.Set<Medico>().ToListAsync();

    public async Task<Medico> CreateAsync(Medico entity)
    {
        _db.Set<Medico>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Medico> UpdateAsync(Medico entity)
    {
        _db.Set<Medico>().Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Medico com id {id} não encontrado.");
        _db.Set<Medico>().Remove(entity);
        await _db.SaveChangesAsync();
    }
}
