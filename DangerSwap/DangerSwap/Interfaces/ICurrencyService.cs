using DangerSwap.Models;

namespace DangerSwap.Interfaces;

public interface ICurrencyService
{
    Task UpsertCurrenciesAsync(bool isFiat);

    Task<Currency> GetCurrencyAsync(string currencyId);
}
