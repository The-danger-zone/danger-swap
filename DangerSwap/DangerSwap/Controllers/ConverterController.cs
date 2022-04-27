using DangerSwap.Models;
using DangerSwap.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DangerSwap.Controllers
{
    [Authorize]
    public class ConverterController : Controller
    {
        private readonly ConverterRepository _converterRepository;
        private readonly UserRepository _userRepository;

        public ConverterController(ConverterRepository converterRepository, UserRepository userRepository)
        {
            _converterRepository = converterRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index(double equalAmount = 0.0)
        {
            var fiatCurrencies = _converterRepository.GetAllCurrencies(true);
            var cryptoCurrencies = _converterRepository.GetAllCurrencies(false);
            ViewBag.FiatCurrencies = fiatCurrencies;
            ViewBag.CryptoCurrencies = cryptoCurrencies;
            ViewBag.EqualAmount = equalAmount.ToString("F99").TrimEnd('0');
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
            return RedirectToAction(nameof(Index), new { equalAmount = convertedEquivalent});
        }
    }
}