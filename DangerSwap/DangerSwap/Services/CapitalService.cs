using DangerSwap.Interfaces;
using DangerSwap.Models;

namespace DangerSwap.Services;

public sealed class CapitalService : ICapitalService
{
    private readonly ICapitalRepository _capitalRepository;

    public CapitalService(ICapitalRepository capitalRepository)
    {
        _capitalRepository = capitalRepository;
    }

    public async Task<IEnumerable<Capital>> GetCapitalsAsync(Guid userId)
    {
        var entities = await _capitalRepository.GetEntitiesByUserIdAsync(userId);
        return entities;
    }

    public async Task DepositCapital(Capital capital, User user)
    {
        capital.UserId = user.Id;
        capital.User = user;
        var capitalEntity = await _capitalRepository.GetEntityByCurrencyId(new Guid(capital.CurrencyId));

        if (capitalEntity is null)
        {
            await _capitalRepository.CreateEntity(capital);
        }
        else
        {
            capitalEntity.Amount += capital.Amount;
            await _capitalRepository.UpdateEntity(capitalEntity);
        }
    }

    public async Task<bool> WithdrawCapital(string currencyId, decimal amount)
    {
        var capital = await _capitalRepository.GetEntityByCurrencyId(new Guid(currencyId));
        if (capital is null) return false;

        capital.Amount -= amount;
        if (capital.Amount < 0) return false;

        if (capital.Amount == 0)
            await _capitalRepository.DeleteEntity(new Guid(capital.CurrencyId));
        else 
            await _capitalRepository.UpdateEntity(capital);
        return true;
    }

    public async Task<bool> ConvertCapital(string sourceCurrencyId, string targetCurrencyId, decimal amount, decimal convertedEquivalent, User user)
    {
        var withdrawSucced = await WithdrawCapital(sourceCurrencyId, amount);
        if (!withdrawSucced) return false;

        var newCapital = new Capital()
        {
            CurrencyId = targetCurrencyId,
            Amount = convertedEquivalent,
            User = user,
            UserId = user.Id,
        };
        await DepositCapital(newCapital, user);
        return true;
    }
}
