using DangerSwap.DbContexts;
using DangerSwap.Interfaces;
using DangerSwap.Models;
using Microsoft.EntityFrameworkCore;

namespace DangerSwap.Repositories;

public sealed class CapitalRepository : ICapitalRepository
{
    private readonly DangerSwapContext _context;

    public CapitalRepository(DangerSwapContext dangerSwapContext)
    {
        _context = dangerSwapContext;
    }

    public async Task CreateEntity(Capital entity)
    {
        _context.Capitals.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEntity(Guid id)
    {
        var entity = await GetEntityByCurrencyId(id);
        _context.Capitals.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Capital>> GetEntitiesAsync()
    {
        var entities = await _context.Capitals.ToListAsync();
        return entities;
    }

    public async Task<IEnumerable<Capital>> GetEntitiesByUserIdAsync(Guid userId)
    {
        var entities = await _context.Capitals
            .Where(q => q.UserId == userId.ToString())
            .Include(q => q.Currency)
            .Include(q => q.Currency!.Rate)
            .Include(q => q.User)
            .ToListAsync();
        return entities;
    }

    public async Task<Capital> GetEntity(Guid id)
    {
        var entity = await _context.Capitals
            .FirstOrDefaultAsync(q => q.UserId == id.ToString());
        if (entity is null)
        {
            throw new ArgumentException(nameof(id));
        }

        return entity;
    }

    public async Task<Capital?> GetEntityByCurrencyId(Guid id)
    {
        var entity = await _context.Capitals
            .Include(q => q.Currency)
            .Include(q => q.Currency!.Rate)
            .Include(q => q.User)
            .FirstOrDefaultAsync(q => q.CurrencyId == id.ToString());
        return entity;
    }

    public Task<bool> IsExist(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateEntity(Capital entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }
}
