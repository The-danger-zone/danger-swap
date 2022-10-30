using DangerSwap.Interfaces;
using DangerSwap.Models;
using DangerSwap.Repositories;

namespace DangerSwap.Services
{
    public sealed class CurrencyService : ICurrencyService
    {
        private readonly CurrencyRepository _currencyRepository;
        private readonly ScrapperService _scrapperService;
        public CurrencyService(CurrencyRepository currencyRepository, ScrapperService scrapperService)
        {
            _currencyRepository = currencyRepository;
            _scrapperService = scrapperService;
        }

        public async Task UpsertCurrenciesAsync(bool isFiat)
        {
            var currencies = _scrapperService.ReadScrappedCurrencies(isFiat);
            if (!currencies.Any())
            {
                return;
            }

            foreach (var item in currencies)
            {
                var originCurrency = await _currencyRepository.GetEntityBySymbol(item.Symbol);
                if (originCurrency is null)
                {
                    await CreateCurrencyAsync(item, isFiat);
                }
                else
                {
                    await UpdateCurrencyAsync(originCurrency, item);
                }
            }
        }

        private async Task CreateCurrencyAsync(ScrappedCurrency scrappedCurrency, bool isFiat)
        {
            var newCurrency = new Currency()
            {
                Symbol = scrappedCurrency.Symbol,
                Name = scrappedCurrency.Name,
                IsFiat = isFiat,
                Rate = new Rate()
                {
                    RateUsd = scrappedCurrency.Price
                }
            };
            await _currencyRepository.CreateEntity(newCurrency);
        }

        private async Task UpdateCurrencyAsync(Currency originCurrency, ScrappedCurrency scrappedCurrency)
        {
            OverrideCurrencyRate(originCurrency, scrappedCurrency);
            await _currencyRepository.UpdateEntity(originCurrency);
        }

        private static void OverrideCurrencyRate(Currency originCurrency, ScrappedCurrency scrappedCurrency)
        {
            originCurrency.Rate!.RateUsd = scrappedCurrency.Price;
        }
    }
}
