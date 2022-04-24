using DangerSwap.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DangerSwap.Models
{
    public class User : IdentityUser
    {
        [Required (ErrorMessage = "The Username field is required")]
        [StringLength(StringLengthConst.defaultString)]
        public override string UserName { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.password)]
        [MinLength(StringLengthConst.minPasswordLength)]
        [DataType(DataType.Password)]

        public string Password { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.email)]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid Email format")]
        public override string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.defaultString)]
        public string Citizenship { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.defaultString)]
        public string Nationality { get; set; } = string.Empty;
        [Required(ErrorMessage ="The Date of Birth is required")]
        public DateTime? BirthDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public ICollection<Transaction>? Transactions { get; set; } 
    }
}
