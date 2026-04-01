using Microsoft.EntityFrameworkCore;
using OmniHealth.API.Data;
using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public class PrescricaoRepository : IPrescricaoRepository
{
    protected readonly OmniHealthDbContext _db;
    public PrescricaoRepository(OmniHealthDbContext db) => _db = db;

    public async Task<Prescricao?> GetByIdAsync(int id)
        => await _db.Set<Prescricao>().FindAsync(id);

    public async Task<IEnumerable<Prescricao>> GetAllAsync()
        => await _db.Set<Prescricao>().ToListAsync();

    public async Task<Prescricao> CreateAsync(Prescricao entity)
    {
        _db.Set<Prescricao>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Prescricao> UpdateAsync(Prescricao entity)
    {
        _db.Set<Prescricao>().Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Prescricao com id {id} não encontrado.");
        _db.Set<Prescricao>().Remove(entity);
        await _db.SaveChangesAsync();
    }
}
