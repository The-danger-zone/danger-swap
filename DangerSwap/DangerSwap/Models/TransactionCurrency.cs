using System.ComponentModel.DataAnnotations.Schema;

namespace DangerSwap.Models
{
    public class TransactionCurrency : BaseEntity
    {
        [ForeignKey("Transaction")]
        public string TransactionId { get; set; } = string.Empty;
        public Transaction? Transaction { get; set; }
        [ForeignKey("FromCurrency")]
        public string FromId { get; set; } = string.Empty;
        public Currency? FromCurrency { get; set; }
        [ForeignKey("ToCurrency")]
        public string ToId { get; set; } = string.Empty;
        public Currency? ToCurrency { get; set; }

    }
}
