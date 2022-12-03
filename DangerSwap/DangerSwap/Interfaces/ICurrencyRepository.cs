using DangerSwap.Models;
using DangerSwap.Repositories;

namespace DangerSwap.Interfaces;

public interface ICurrencyRepository : IRepository<Currency>
{
    Task<Currency?> GetEntityBySymbol(string symbol);
}
