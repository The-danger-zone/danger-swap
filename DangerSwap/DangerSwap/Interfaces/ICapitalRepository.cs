using DangerSwap.Models;
using DangerSwap.Repositories;

namespace DangerSwap.Interfaces;

public interface ICapitalRepository : IRepository<Capital>
{
    Task<Capital?> GetEntityByCurrencyId(Guid id);

    Task<IEnumerable<Capital>> GetEntitiesByUserIdAsync(Guid userId);
}
