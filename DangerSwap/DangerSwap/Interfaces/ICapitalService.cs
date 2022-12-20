using DangerSwap.Models;

namespace DangerSwap.Interfaces;

public interface ICapitalService
{
    Task DepositCapital(Capital capital, User user);

    Task<IEnumerable<Capital>> GetCapitalsAsync(Guid userId);
    
    Task<bool> WithdrawCapital(string capitalId, decimal amount);

    Task<bool> ConvertCapital(string sourceCurrencyId, string targetCurrencyId, decimal amount, decimal convertedEquivalent, User user);
}