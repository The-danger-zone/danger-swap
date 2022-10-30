using DangerSwap.Models;
using DangerSwap.Repositories;

namespace DangerSwap.Services
{
    public class CurrencyService
    {
        // Ernest
        private readonly CurrencyRepository _currencyRepository;
        private readonly ScrapperService _scrapperService;
        public CurrencyService(CurrencyRepository currencyRepository, ScrapperService scrapperService)
        {
            _currencyRepository = currencyRepository;
            _scrapperService = scrapperService;
        }

        public async Task UploadCurrencies(bool isFiat)
        {
            var currencies = _scrapperService.ReadScrappedCurrencies(isFiat);
            if (currencies != null)
            {
                foreach (var item in currencies)
                {
                    var originCurrency = await _currencyRepository.GetEntityBySymbol(item.Symbol);
                    if(originCurrency == null)
                    {
                        await CreateCurrency(item, isFiat);
                    } else
                    {
                        await UpdateCurrency(originCurrency, item);
                    }
                }
            }
        }

        private async Task CreateCurrency(ScrappedCurrency scrappedCurrency, bool isFiat)
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

        private async Task UpdateCurrency(Currency originCurrency, ScrappedCurrency scrappedCurrency)
        {
            OverrideCurrencyRate(originCurrency, scrappedCurrency);
            await _currencyRepository.UpdateEntity(originCurrency);
        }

        private void OverrideCurrencyRate(Currency originCurrency, ScrappedCurrency scrappedCurrency)
        {
            originCurrency.Rate.RateUsd = scrappedCurrency.Price;
        }
    }
}
