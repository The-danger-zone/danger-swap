using System.ComponentModel.DataAnnotations.Schema;

namespace DangerSwap.Models
{
    public class TransactionCurrency : BaseEntity
    {
        [ForeignKey("Transaction")]
        public string TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        [ForeignKey("FromCurrency")]
        public string FromId { get; set; }
        public Currency FromCurrency { get; set; }
        [ForeignKey("ToCurrency")]
        public string ToId { get; set; }
        public Currency ToCurrency { get; set; }

    }
}
