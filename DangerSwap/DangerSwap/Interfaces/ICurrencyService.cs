using DangerSwap.Models;

namespace DangerSwap.Interfaces;

public interface ICurrencyService
{
    Task UpsertCurrenciesAsync();

    Task<Currency> GetCurrencyAsync(string currencyId);
}
