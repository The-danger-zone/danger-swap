using DangerSwap.Models;
using DangerSwap.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DangerSwap.Interfaces;

namespace DangerSwap.Controllers;

[Authorize]
public class ConverterController : Controller
{
    private readonly IConverterRepository _converterRepository;
    private readonly IUserRepository _userRepository;
    private readonly IScrapperService _scrapperService;
    private readonly ICurrencyService _currencyService;

    public ConverterController(IConverterRepository converterRepository, IUserRepository userRepository, IScrapperService scrapperService, ICurrencyService currencyService)
    {
        _converterRepository = converterRepository;
        _userRepository = userRepository;
        _scrapperService = scrapperService;
        _currencyService = currencyService;
    }

    //TODO: make a task manager to run scrappers
    public async Task<IActionResult> Index(double equalAmount = 0.0)
    {
        _scrapperService.RunScrappers();

        var fiatCurrencies = _converterRepository.GetAllCurrencies(true);
        var cryptoCurrencies = _converterRepository.GetAllCurrencies(false);
        ViewBag.FiatCurrencies = fiatCurrencies;
        ViewBag.CryptoCurrencies = cryptoCurrencies;
        ViewBag.EqualAmount = equalAmount.ToString("F99").TrimEnd('0');

        await _currencyService.UpsertCurrenciesAsync(true);
        await _currencyService.UpsertCurrenciesAsync(false);

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Convert(Transaction transaction)
    {
        decimal convertedEquivalent = default;
        if (ModelState.IsValid)
        {
            string username = User?.Identity?.Name ?? string.Empty;
            var user = await _userRepository.GetEntityByUsername(username);
            transaction.User = user;
            await _converterRepository.CreateTransaction(transaction);
            convertedEquivalent = _converterRepository.Convert(transaction);
        }

        return RedirectToAction(nameof(Index), new { equalAmount = convertedEquivalent });
    }

    [HttpGet("currencies/{id}")]
    public async Task<IActionResult> GetCurrencyInformation(string id)
    {
        var currency = await _currencyService.GetCurrencyAsync(id);
        if (currency is null)
        {
            return NotFound();
        }

        return Ok(currency);
    }
}