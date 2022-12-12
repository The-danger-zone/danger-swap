using DangerSwap.Interfaces;
using DangerSwap.Models;

namespace DangerSwap.Services;

public sealed class ConverterService : IConverterService
{
    private readonly IConverterRepository _converterRepository;

    public ConverterService(IConverterRepository converterRepository)
    {
        _converterRepository = converterRepository;
    }

    public async Task<decimal> ConvertCurrency(Transaction transaction)
    {
        await _converterRepository.CreateTransaction(transaction);
        var convertedEquivalent = _converterRepository.Convert(transaction);
        return convertedEquivalent;
    }
}
