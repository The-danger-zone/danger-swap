﻿using DangerSwap.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DangerSwap.Models
{
    public class Currency : BaseEntity
    {
        [Required]
        [StringLength(StringLengthConst.Symbol)]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.DefaultString)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public bool IsFiat { get; set; }
        [StringLength(StringLengthConst.Description)]
        public string Description { get; set; } = string.Empty;
        [ForeignKey("Rate")]
        public string? RateId { get; set; }
        public Rate? Rate { get; set; }
    }
}
