using Microsoft.EntityFrameworkCore;
using OmniHealth.API.Data;
using OmniHealth.API.Models;

namespace OmniHealth.API.Repositories;

public class FuncionarioRepository : IFuncionarioRepository
{
    protected readonly OmniHealthDbContext _db;
    public FuncionarioRepository(OmniHealthDbContext db) => _db = db;

    public async Task<Funcionario?> GetByIdAsync(int id)
        => await _db.Set<Funcionario>().FindAsync(id);

    public async Task<IEnumerable<Funcionario>> GetAllAsync()
        => await _db.Set<Funcionario>().ToListAsync();

    public async Task<Funcionario> CreateAsync(Funcionario entity)
    {
        _db.Set<Funcionario>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Funcionario> UpdateAsync(Funcionario entity)
    {
        _db.Set<Funcionario>().Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Funcionario com id {id} não encontrado.");
        _db.Set<Funcionario>().Remove(entity);
        await _db.SaveChangesAsync();
    }
}
