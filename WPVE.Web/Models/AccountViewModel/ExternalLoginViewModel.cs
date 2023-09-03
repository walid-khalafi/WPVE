using System;
using System.ComponentModel.DataAnnotations;

namespace WPVE.Web.Models.AccountViewModel
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

