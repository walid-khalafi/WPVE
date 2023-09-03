using System;
using System.ComponentModel.DataAnnotations;

namespace WPVE.Web.Areas.Admin.Models.ManageViewModels
{
    public class IndexViewModel:WPVE.Services.Users.Profile
    {
        public string StatusMessage { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; }
    }
}
