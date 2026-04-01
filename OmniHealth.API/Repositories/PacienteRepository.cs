using Microsoft.EntityFrameworkCore;
using OmniHealth.API.Data;
using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public class PacienteRepository : IPacienteRepository
{
    protected readonly OmniHealthDbContext _db;
    public PacienteRepository(OmniHealthDbContext db) => _db = db;

    public async Task<Paciente?> GetByIdAsync(int id)
        => await _db.Set<Paciente>().FindAsync(id);

    public async Task<IEnumerable<Paciente>> GetAllAsync()
        => await _db.Set<Paciente>().ToListAsync();

    public async Task<Paciente> CreateAsync(Paciente entity)
    {
        _db.Set<Paciente>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Paciente> UpdateAsync(Paciente entity)
    {
        _db.Set<Paciente>().Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Paciente com id {id} não encontrado.");
        _db.Set<Paciente>().Remove(entity);
        await _db.SaveChangesAsync();
    }
}
