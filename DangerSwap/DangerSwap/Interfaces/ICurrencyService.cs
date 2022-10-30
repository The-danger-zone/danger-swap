namespace DangerSwap.Interfaces
{
    public interface ICurrencyService
    {
        Task UpsertCurrenciesAsync(bool isFiat);
    }
}
