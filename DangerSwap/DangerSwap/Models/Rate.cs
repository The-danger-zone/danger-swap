using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DangerSwap.Models
{
    public class Rate : BaseEntity
    {
        [Required]
        public double RateUsd { get; set; } 

    }
}
