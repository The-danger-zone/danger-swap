namespace DangerSwap.Models
{
    // Model is used only for Json deserialization 
    public class ScrappedCurrency
    {
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Change { get; set; } = string.Empty;
    }
}
