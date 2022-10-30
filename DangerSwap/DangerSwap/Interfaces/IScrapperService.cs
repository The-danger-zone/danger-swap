using DangerSwap.Models;

namespace DangerSwap.Interfaces
{
    public interface IScrapperService
    {
        void RunScrappers();
        public IEnumerable<ScrappedCurrency>? ReadScrappedCurrencies(bool isFiat);
    }
}
