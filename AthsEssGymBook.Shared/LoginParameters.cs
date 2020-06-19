using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AthsEssGymBook.Shared
{
    public class LoginParameters
    {
        [Required]
        public string Mobile { get; set; }

        [Required]
        public string Pin { get; set; }

        public bool RememberMe { get; set; }

        public string UserName
        {
            get; set;
        }
        public string Password
        {
            get; set;
        }
    }
}
