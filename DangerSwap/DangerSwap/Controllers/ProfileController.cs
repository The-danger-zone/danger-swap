using DangerSwap.Interfaces;
using DangerSwap.Models;
using DangerSwap.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DangerSwap.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICapitalService _capitalService;
        private readonly IConverterRepository _converterRepository;

        public ProfileController(IUserService userService, IConverterRepository converterRepository, ICapitalService capitalService)
        {
            _userService = userService;
            _converterRepository = converterRepository;
            _capitalService = capitalService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUser(User);
            var capitals = await _capitalService.GetCapitalsAsync(new Guid(user.Id));
            ViewBag.Capitals = capitals;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateableUser updateableUser)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            await _userService.UpdateUserInfo(updateableUser, User);

            return RedirectToAction(nameof(Index), "Converter");
        }

        [HttpGet]
        public IActionResult DepositCapital()
        {
            var fiatCurrencies = _converterRepository.GetAllCurrencies(true);
            var cryptoCurrencies = _converterRepository.GetAllCurrencies(false);
            var currencies = fiatCurrencies.Concat(cryptoCurrencies);
            ViewBag.Currencies = currencies;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DepositCapital(Capital capital)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Capital), "Profile");
            }
            var user = await _userService.GetUser(User);
            await _capitalService.DepositCapital(capital, user);
            return RedirectToAction(nameof(Index), "Profile");
        }

        [HttpGet]
        public async Task<IActionResult> WithdrawCapital()
        {
            var user = await _userService.GetUser(User);
            var fiatCurrencies = _converterRepository.GetAllCurrencies(true);
            var cryptoCurrencies = _converterRepository.GetAllCurrencies(false);
            var currencies = fiatCurrencies.Concat(cryptoCurrencies);
            var capitals = await _capitalService.GetCapitalsAsync(new Guid(user.Id));
            ViewBag.Currencies = currencies;
            ViewBag.Capitals = capitals;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WithdrawCapital(Capital capital)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Capital), "Profile");
            }

            var user = await _userService.GetUser(User);
            var success = await _capitalService.WithdrawCapital(capital.CurrencyId, capital.Amount);

            return RedirectToAction(nameof(Index), "Profile");
        }
    }
}
