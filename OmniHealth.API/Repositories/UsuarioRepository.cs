using Microsoft.EntityFrameworkCore;
using OmniHealth.API.Data;
using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    protected readonly OmniHealthDbContext _db;
    public UsuarioRepository(OmniHealthDbContext db) => _db = db;

    public async Task<Usuario?> GetByIdAsync(int id)
        => await _db.Set<Usuario>().FindAsync(id);

    public async Task<IEnumerable<Usuario>> GetAllAsync()
        => await _db.Set<Usuario>().ToListAsync();

    public async Task<Usuario> CreateAsync(Usuario entity)
    {
        _db.Set<Usuario>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Usuario> UpdateAsync(Usuario entity)
    {
        _db.Set<Usuario>().Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Usuario com id {id} não encontrado.");
        _db.Set<Usuario>().Remove(entity);
        await _db.SaveChangesAsync();
    }
}
