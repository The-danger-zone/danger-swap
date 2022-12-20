using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DangerSwap.Models;

public sealed class Capital : BaseEntity
{
    public string CurrencyId { get; set; } = null!;

    public Currency? Currency { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [ForeignKey("User")]
    public string? UserId { get; set; }
    public User? User { get; set; }
}
