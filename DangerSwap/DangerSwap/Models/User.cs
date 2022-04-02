using DangerSwap.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DangerSwap.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(StringLengthConst.defaultString)]
        public string Username { get; set; } = null!;
        [Required]
        [StringLength(StringLengthConst.defaultString)]
        [MinLength(StringLengthConst.minPasswordLength)]
        public string Password { get; set; } = null!;
        [Required]
        [StringLength(StringLengthConst.defaultString)]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(StringLengthConst.defaultString)]
        public string Citizenship { get; set; } = null!;
        [Required]
        [StringLength(StringLengthConst.defaultString)]
        public string Nationality { get; set; } = null!;
        [Required]
        public DateTime BirthDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
