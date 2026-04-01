using Microsoft.EntityFrameworkCore;
using OmniHealth.API.Data;
using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public class MedicamentoRepository : IMedicamentoRepository
{
    protected readonly OmniHealthDbContext _db;
    public MedicamentoRepository(OmniHealthDbContext db) => _db = db;

    public async Task<Medicamento?> GetByIdAsync(int id)
        => await _db.Set<Medicamento>().FindAsync(id);

    public async Task<IEnumerable<Medicamento>> GetAllAsync()
        => await _db.Set<Medicamento>().ToListAsync();

    public async Task<Medicamento> CreateAsync(Medicamento entity)
    {
        _db.Set<Medicamento>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Medicamento> UpdateAsync(Medicamento entity)
    {
        _db.Set<Medicamento>().Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Medicamento com id {id} não encontrado.");
        _db.Set<Medicamento>().Remove(entity);
        await _db.SaveChangesAsync();
    }
}
