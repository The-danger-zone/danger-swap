using DangerSwap.Models;

namespace DangerSwap.Interfaces
{
    public interface IScrapperService
    {
        void RunScrappers();
        IEnumerable<ScrappedCurrency> ReadScrappedCurrencies(bool isFiat);
    }
}
