using DangerSwap.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DangerSwap.Models
{
    public class Currency : BaseEntity
    {
        [Required]
        [StringLength(StringLengthConst.symbol)]
        public string Symbol { get; set; }
        [Required]
        [StringLength(StringLengthConst.defaultString)]
        public string Name { get; set; }
        [Required]
        public bool IsFiat { get; set; }
        [StringLength(StringLengthConst.description)]
        public string Description { get; set; }
        [ForeignKey("Rate")]
        public string RateId { get; set; }
        public Rate Rate { get; set; }
    }
}
