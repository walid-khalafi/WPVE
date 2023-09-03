using System;
using System.ComponentModel.DataAnnotations;

namespace WPVE.Core.Domain.Users
{
    public class Profile
    {
        public Profile()
        {
        }

        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public string ThemeColor { get; set; }
        public string NavigationSize { get; set; }
    }
}

