using DangerSwap.DbContexts;
using DangerSwap.Interfaces;
using DangerSwap.Models;
using Microsoft.EntityFrameworkCore;

namespace DangerSwap.Repositories;

public sealed class ConverterRepository : IConverterRepository
{
    private readonly DangerSwapContext _dbContext;

    public ConverterRepository(DangerSwapContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public IEnumerable<Currency> GetAllCurrencies(bool isFiat)
    {
        return _dbContext.Currencies
            .Where(q => q.IsFiat == isFiat)
            .Include(q => q.Rate)
            .ToList();
    }

    public async Task CreateTransaction(Transaction converterTransaction)
    {
        using var transaction = _dbContext.Database.BeginTransaction();
        try
        {
            converterTransaction.Rate = GetRate(converterTransaction);
            await _dbContext.Transactions.AddAsync(converterTransaction);
            await _dbContext.SaveChangesAsync();
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
    }

    public decimal Convert(Transaction transaction)
    {
        decimal fiatEquivalentAmount = GetFiatEquivalent(transaction);
        return CalculateCryptoEquivalent(fiatEquivalentAmount, transaction);
    }

    private decimal GetFiatEquivalent(Transaction transaction)
    {
        var fromCurrencyRate = _dbContext
            .Currencies
            .Include(e => e.Rate)
            .First(e => e.Id == transaction
            .TransactionCurrency!
            .FromId)
            .Rate?
            .RateUsd;
        return (decimal)(fromCurrencyRate.GetValueOrDefault() * (double)transaction.Amount.GetValueOrDefault());
    }

    private static decimal CalculateCryptoEquivalent(decimal fiatEquivalentAmount, Transaction transaction)
    {
        var cryptoRateUsd = transaction?.Rate;
        return fiatEquivalentAmount / (decimal)cryptoRateUsd.GetValueOrDefault();
    }

    private double GetRate(Transaction transaction)
    {
        return _dbContext.Currencies
            .Include(e => e.Rate)
            .First(q => q.Id == transaction.TransactionCurrency!.ToId)
            .Rate!.RateUsd;
    }
}
