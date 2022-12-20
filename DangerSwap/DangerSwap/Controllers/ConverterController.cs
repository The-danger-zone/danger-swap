using DangerSwap.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DangerSwap.Interfaces;
using DangerSwap.Models.ViewModel;

namespace DangerSwap.Controllers;

[Authorize]
public class ConverterController : Controller
{
    private readonly IConverterRepository _converterRepository;
    private readonly IUserService _userService;
    private readonly IScrapperService _scrapperService;
    private readonly ICurrencyService _currencyService;
    private readonly IConverterService _converterService;
    private readonly ICapitalService _capitalService;

    public ConverterController(IConverterRepository converterRepository, IScrapperService scrapperService, ICurrencyService currencyService, IUserService userService, IConverterService converterService, ICapitalService capitalService)
    {
        _converterRepository = converterRepository;
        _scrapperService = scrapperService;
        _currencyService = currencyService;
        _userService = userService;
        _converterService = converterService;
        _capitalService = capitalService;
    }

    //TODO: make a task manager to run scrappers
    public async Task<IActionResult> Index(double equalAmount = 0.0, decimal convertableAmount = decimal.Zero)
    {
        _scrapperService.RunScrappers();

        var fiatCurrencies = _converterRepository.GetAllCurrencies(true);
        var cryptoCurrencies = _converterRepository.GetAllCurrencies(false);
        var equalAmountString = equalAmount == 0.0 ? "0" : equalAmount.ToString("F5");
        ViewBag.FiatCurrencies = fiatCurrencies;
        ViewBag.CryptoCurrencies = cryptoCurrencies;
        ViewBag.EqualAmount = equalAmountString;
        ViewBag.ConvertableAmount = convertableAmount;

        await _currencyService.UpsertCurrenciesAsync();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Convert(TransactionViewModel transactionViewModel)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index), new { equalAmount = 0, convertableAmount = 0 });
        }

        var transaction = transactionViewModel.Transaction;
        var user = await _userService.GetUser(User);
        transaction.User = user;
        var convertedEquivalent = await _converterService.ConvertCurrency(transaction);
        if (!transactionViewModel.IsCapitalUsed)
            return RedirectToAction(nameof(Index), new { equalAmount = convertedEquivalent, convertableAmount = transaction.Amount });

        var isConverted = await _capitalService.ConvertCapital(transaction.TransactionCurrency!.FromId, transaction.TransactionCurrency!.ToId, transaction.Amount.GetValueOrDefault(), convertedEquivalent, user);
        if (!isConverted)
        {
            ModelState.AddModelError("Error", "Capital is not valid!");
            return RedirectToAction(nameof(Index), new { equalAmount = 0, convertableAmount = 0 });
        }
        return RedirectToAction(nameof(Index), new { equalAmount = convertedEquivalent, convertableAmount = transaction.Amount});
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
