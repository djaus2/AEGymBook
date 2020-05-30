using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AthsEssGymBook.Shared
{
    public class RegisterParameters
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string PasswordConfirm { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Compare(nameof(Email), ErrorMessage = "Emails do not match")]
        public string EmailConfirm { get; set; }

        public string Phone { get; set; }
    }
}
