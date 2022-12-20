using DangerSwap.Interfaces;
using DangerSwap.Models;

namespace DangerSwap.Services;

public sealed class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IScrapperService _scrapperService;
    public CurrencyService(ICurrencyRepository currencyRepository, IScrapperService scrapperService)
    {
        _currencyRepository = currencyRepository;
        _scrapperService = scrapperService;
    }

    public async Task UpsertCurrenciesAsync()
    {
        var currencies = _scrapperService.ReadScrappedCurrencies();

        await InsertOrUpdateCurrencies(currencies.Fiat, true);
        await InsertOrUpdateCurrencies(currencies.Crypto, false);
    }

    public async Task<Currency> GetCurrencyAsync(string currencyId)
    {
        var currency = await _currencyRepository.GetEntity(new Guid(currencyId));

        return currency;
    }

    private async Task InsertOrUpdateCurrencies(IEnumerable<ScrappedCurrency> scrappedCurrencies, bool isFiat)
    {
        if (!scrappedCurrencies.Any())
        {
            return;
        }

        foreach (var item in scrappedCurrencies)
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

