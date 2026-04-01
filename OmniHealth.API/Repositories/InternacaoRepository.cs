using Microsoft.EntityFrameworkCore;
using OmniHealth.API.Data;
using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public class InternacaoRepository : IInternacaoRepository
{
    protected readonly OmniHealthDbContext _db;
    public InternacaoRepository(OmniHealthDbContext db) => _db = db;

    public async Task<Internacao?> GetByIdAsync(int id)
        => await _db.Set<Internacao>().FindAsync(id);

    public async Task<IEnumerable<Internacao>> GetAllAsync()
        => await _db.Set<Internacao>().ToListAsync();

    public async Task<Internacao> CreateAsync(Internacao entity)
    {
        _db.Set<Internacao>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Internacao> UpdateAsync(Internacao entity)
    {
        _db.Set<Internacao>().Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Internacao com id {id} não encontrado.");
        _db.Set<Internacao>().Remove(entity);
        await _db.SaveChangesAsync();
    }
}
