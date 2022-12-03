using DangerSwap.DbContexts;
using DangerSwap.Models;
using Microsoft.EntityFrameworkCore;

namespace DangerSwap.Repositories
{
    public class ConverterRepository 
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
            } catch (Exception)
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
                .TransactionCurrency
                .FromId)
                .Rate?
                .RateUsd;
            return (decimal)(fromCurrencyRate * (double?)transaction.Amount);
        }

        private decimal CalculateCryptoEquivalent(decimal fiatEquivalentAmount, Transaction transaction)
        {
            var cryptoRateUsd = transaction?.Rate;
            return fiatEquivalentAmount / (decimal)cryptoRateUsd;
        }

        private double GetRate(Transaction transaction)
        {
            return _dbContext.Currencies
                .Include(e => e.Rate)
                .First(q => q.Id == transaction.TransactionCurrency!.ToId)
                .Rate!.RateUsd;
        }
    }
}
