using DangerSwap.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace DangerSwap.Models
{
    public class UpdateableUser
    {
        [Required]
        [StringLength(StringLengthConst.email)]
        public string NewEmail { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.email)]
        public string ConfirmedNewEmail { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.password)]
        [MinLength(StringLengthConst.minPasswordLength)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.password)]
        [MinLength(StringLengthConst.minPasswordLength)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.password)]
        [MinLength(StringLengthConst.minPasswordLength)]
        [DataType(DataType.Password)]
        public string ConfirmedNewPassword { get; set; } = string.Empty;
    }
}
