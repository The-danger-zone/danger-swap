using DangerSwap.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DangerSwap.Models
{
    public sealed class Currency : BaseEntity
    {
        [Required]
        [StringLength(StringLengthConst.symbol)]
        public string Symbol { get; set; } = null!;
        [Required]
        [StringLength(StringLengthConst.defaultString)]
        public string Name { get; set; } = null!;
        [Required]
        public bool IsFiat { get; set; }
        [StringLength(StringLengthConst.description)]
        public string Description { get; set; } = string.Empty;

        [ForeignKey("Rate")] 
        public string? RateId { get; set; } = null!;
        public Rate? Rate { get; set; }
    }
}
