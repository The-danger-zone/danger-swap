using DangerSwap.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DangerSwap.Interfaces;

namespace DangerSwap.Controllers;

[Authorize]
public class ConverterController : Controller
{
    private readonly IConverterRepository _converterRepository;
    private readonly IUserService _userService;
    private readonly IScrapperService _scrapperService;
    private readonly ICurrencyService _currencyService;
    private readonly IConverterService _converterService;

    public ConverterController(IConverterRepository converterRepository, IScrapperService scrapperService, ICurrencyService currencyService, IUserService userService, IConverterService converterService)
    {
        _converterRepository = converterRepository;
        _scrapperService = scrapperService;
        _currencyService = currencyService;
        _userService = userService;
        _converterService = converterService;
    }

    //TODO: make a task manager to run scrappers
    public async Task<IActionResult> Index(double equalAmount = 0.0)
    {
        _scrapperService.RunScrappers();

        var fiatCurrencies = _converterRepository.GetAllCurrencies(true);
        var cryptoCurrencies = _converterRepository.GetAllCurrencies(false);
        var equalAmountString = equalAmount == 0.0 ? "0" : equalAmount.ToString("F5");
        ViewBag.FiatCurrencies = fiatCurrencies;
        ViewBag.CryptoCurrencies = cryptoCurrencies;
        ViewBag.EqualAmount = equalAmountString;

        await _currencyService.UpsertCurrenciesAsync();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Convert(Transaction transaction)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Index), new { equalAmount = decimal.Zero });

        var user = await _userService.GetUser(User);
        transaction.User = user;
        var convertedEquivalent = await _converterService.ConvertCurrency(transaction);

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
