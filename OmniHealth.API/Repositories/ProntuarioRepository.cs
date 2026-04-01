using Microsoft.EntityFrameworkCore;
using OmniHealth.API.Data;
using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public class ProntuarioRepository : IProntuarioRepository
{
    protected readonly OmniHealthDbContext _db;
    public ProntuarioRepository(OmniHealthDbContext db) => _db = db;

    public async Task<Prontuario?> GetByIdAsync(int id)
        => await _db.Set<Prontuario>().FindAsync(id);

    public async Task<IEnumerable<Prontuario>> GetAllAsync()
        => await _db.Set<Prontuario>().ToListAsync();

    public async Task<Prontuario> CreateAsync(Prontuario entity)
    {
        _db.Set<Prontuario>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Prontuario> UpdateAsync(Prontuario entity)
    {
        _db.Set<Prontuario>().Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Prontuario com id {id} não encontrado.");
        _db.Set<Prontuario>().Remove(entity);
        await _db.SaveChangesAsync();
    }
}
