using DangerSwap.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DangerSwap.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(StringLengthConst.DefaultString)]
        public override string UserName { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.Password)]
        [MinLength(StringLengthConst.MinPasswordLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.Email)]
        public override string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.DefaultString)]
        public string Citizenship { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.DefaultString)]
        public string Nationality { get; set; } = string.Empty;
        [Required]
        public DateTime BirthDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public ICollection<Transaction>? Transactions { get; set; } 
    }
}
