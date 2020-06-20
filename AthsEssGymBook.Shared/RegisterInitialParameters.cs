using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Text;

namespace AthsEssGymBook.Shared
{
    public class RegisterInitialParameters
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Pin { get; set; }

        [Required]
        public string Mobile { get; set; }

        public bool IsMember2019 { get; set; } = false;

        public bool IsMember2020 { get; set; } = false;
    }
}
