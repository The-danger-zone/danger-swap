using DangerSwap.Models;

namespace DangerSwap.Interfaces;

public interface IConverterRepository
{
    IEnumerable<Currency> GetAllCurrencies(bool isFiat);

    Task CreateTransaction(Transaction converterTransaction);

    decimal Convert(Transaction transaction);
}
