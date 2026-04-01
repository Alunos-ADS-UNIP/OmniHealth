using Microsoft.EntityFrameworkCore;
using OmniHealth.API.Data;
using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public class ConsultaRepository : IConsultaRepository
{
    protected readonly OmniHealthDbContext _db;
    public ConsultaRepository(OmniHealthDbContext db) => _db = db;

    public async Task<Consulta?> GetByIdAsync(int id)
        => await _db.Set<Consulta>().FindAsync(id);

    public async Task<IEnumerable<Consulta>> GetAllAsync()
        => await _db.Set<Consulta>().ToListAsync();

    public async Task<Consulta> CreateAsync(Consulta entity)
    {
        _db.Set<Consulta>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Consulta> UpdateAsync(Consulta entity)
    {
        _db.Set<Consulta>().Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Consulta com id {id} não encontrado.");
        _db.Set<Consulta>().Remove(entity);
        await _db.SaveChangesAsync();
    }
}
