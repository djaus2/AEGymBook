using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Text;

namespace AthsEssGymBook.Shared
{
    public class RegisterParameters
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Pin { get; set; }

        [Required]
        [Compare(nameof(Pin), ErrorMessage = "Pins do not match")]
        public string PinConfirm { get; set; }


        public string EmailConfirm { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required]
        [IgnoreDataMember]
        [Compare(nameof(Mobile), ErrorMessage = "Mobiles do not match")]
        public string MobileConfirm { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Phone
        {
            get; set;
        }


        public bool IsMember2019 { get; set; } = false;
        public bool IsMember2020 { get; set; } = false;
    }
}
