using DangerSwap.Models;

namespace DangerSwap.Interfaces
{
    public interface IScrapperService
    {
        void RunScrappers();
        ScrappedRates ReadScrappedCurrencies();
    }
}
