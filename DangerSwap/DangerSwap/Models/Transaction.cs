using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DangerSwap.Models
{
    public class Transaction : BaseEntity
    {
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public TransactionCurrency? TransactionCurrency { get; set; }

    }
}
