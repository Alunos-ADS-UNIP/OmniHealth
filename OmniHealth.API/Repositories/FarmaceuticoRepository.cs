using Microsoft.EntityFrameworkCore;
using OmniHealth.API.Data;
using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public class FarmaceuticoRepository : IFarmaceuticoRepository
{
    protected readonly OmniHealthDbContext _db;
    public FarmaceuticoRepository(OmniHealthDbContext db) => _db = db;

    public async Task<Farmaceutico?> GetByIdAsync(int id)
        => await _db.Set<Farmaceutico>().FindAsync(id);

    public async Task<IEnumerable<Farmaceutico>> GetAllAsync()
        => await _db.Set<Farmaceutico>().ToListAsync();

    public async Task<Farmaceutico> CreateAsync(Farmaceutico entity)
    {
        _db.Set<Farmaceutico>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Farmaceutico> UpdateAsync(Farmaceutico entity)
    {
        _db.Set<Farmaceutico>().Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Farmaceutico com id {id} não encontrado.");
        _db.Set<Farmaceutico>().Remove(entity);
        await _db.SaveChangesAsync();
    }
}
