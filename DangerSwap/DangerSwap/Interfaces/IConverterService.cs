using DangerSwap.Models;

namespace DangerSwap.Interfaces;

public interface IConverterService
{
    Task<decimal> ConvertCurrency(Transaction transaction);
}
