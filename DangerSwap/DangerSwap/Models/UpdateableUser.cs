using DangerSwap.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace DangerSwap.Models
{
    public class UpdateableUser
    {
        [Required]
        [StringLength(StringLengthConst.Email)]
        public string NewEmail { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.Email)]
        public string ConfirmedNewEmail { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.Password)]
        [MinLength(StringLengthConst.MinPasswordLength)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.Password)]
        [MinLength(StringLengthConst.MinPasswordLength)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
        [Required]
        [StringLength(StringLengthConst.Password)]
        [MinLength(StringLengthConst.MinPasswordLength)]
        [DataType(DataType.Password)]
        public string ConfirmedNewPassword { get; set; } = string.Empty;
    }
}
