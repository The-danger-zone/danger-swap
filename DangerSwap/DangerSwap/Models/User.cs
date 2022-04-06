using DangerSwap.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DangerSwap.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(StringLengthConst.defaultString)]
        public override string UserName { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.password)]
        [MinLength(StringLengthConst.minPasswordLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.email)]
        public override string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.defaultString)]
        public string Citizenship { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.defaultString)]
        public string Nationality { get; set; } = string.Empty;
        [Required]
        public DateTime BirthDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public ICollection<Transaction>? Transactions { get; set; } 
    }
}
