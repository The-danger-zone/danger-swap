namespace DangerSwap.Models.ViewModel;

public sealed class TransactionViewModel
{
    public Transaction Transaction { get; set; } = null!;

    public bool IsCapitalUsed { get; set; }
}
