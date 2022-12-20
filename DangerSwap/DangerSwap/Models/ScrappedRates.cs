namespace DangerSwap.Models;
// Model is used only for Json deserialization 
public sealed class ScrappedRates
{
    public ICollection<ScrappedCurrency> Fiat { get; set; } = new List<ScrappedCurrency>();

    public ICollection<ScrappedCurrency> Crypto { get; set; } = new List<ScrappedCurrency>();

}
