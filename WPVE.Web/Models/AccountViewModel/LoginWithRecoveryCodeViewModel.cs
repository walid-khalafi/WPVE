using System;
using System.ComponentModel.DataAnnotations;

namespace WPVE.Web.Models.AccountViewModel
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Recovery Code")]
        public string RecoveryCode { get; set; }

    }
}

