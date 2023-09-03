using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WPVE.Services.Users
{
    public class Profile
    {
        public Profile()
        {
        }
        /// <summary>
        /// Gets or sets the User identifier
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the user first name
        /// </summary>
        [Display(Name = "نام")]
        public string First_Name { get; set; }
        /// <summary>
        /// Gets or sets the user last name
        /// </summary>
        [Display(Name = "نام خانوادگی")]
        public string Last_Name { get; set; }
        /// <summary>
        /// Gets or sets the username
        /// </summary>
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets the user email
        /// </summary>
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the user phone number
        /// </summary>
        [Display(Name = "تلفن همراه")]
        public string Phonenumber { get; set; }
        /// <summary>
        /// Gets or sets the user avatar
        /// </summary>
        [Display(Name = "تصویر")]
        public string Avatar { get; set; }
        /// <summary>
        /// Gets or sets the user default theme
        /// </summary>
        [Display(Name = "تم")]
        public string Theme_Color { get; set; }
        /// <summary>
        /// Gets or sets the default user navigation size
        /// </summary>
        public string navigation_size { get; set; }
        /// <summary>
        /// Gets or sets the user role 
        /// </summary>
        [Display(Name = "سطح دسترسی")]
        public string Role { get; set; }
        /// <summary>
        /// Gets  the user full name
        /// </summary>
        public string Full_Name { get { return string.Format("{0} {1}", First_Name, Last_Name); } }
        /// <summary>
        /// Gets or sets the two factor enabled
        /// </summary>
        public bool TwoFactorEnabled { get; set; }
    }
}

