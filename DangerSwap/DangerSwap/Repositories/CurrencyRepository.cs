using DangerSwap.DbContexts;
using DangerSwap.Models;
using Microsoft.EntityFrameworkCore;

namespace DangerSwap.Repositories
{
    public class CurrencyRepository : IRepository<Currency>
    {
        private readonly DangerSwapContext _dbContext;

        public CurrencyRepository(DangerSwapContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateEntity(Currency entity)
        {
            _dbContext.Currencies.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEntity(Guid id)
        {
            var entity = await GetEntity(id);
            _dbContext.Currencies.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Currency>> GetEntitiesAsync()
        {
            return await _dbContext.Currencies.ToListAsync();
        }

        public Task<Currency> GetEntity(Guid id)
        {
            return _dbContext.Currencies
                .Include(q => q.Rate)
                .FirstAsync(e => e.Id == id.ToString());
        }

        public Task<Currency?> GetEntityBySymbol(string symbol)
        {
            return _dbContext.Currencies.FirstOrDefaultAsync(e => e.Symbol == symbol);
        }

        public async Task<bool> IsExist(Guid id)
        {
            return await _dbContext.Currencies.AnyAsync(e => e.Id == id.ToString());
        }

        public async Task UpdateEntity(Currency entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
