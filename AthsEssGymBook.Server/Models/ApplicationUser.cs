using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthsEssGymBook.Server.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; } = "";
        public string Mobile { get; set; } = "";
        public bool IsMember { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
        public bool IsCoach { get; set; } = false;
        public bool HasAccessCard { get; set; } = false;
        public bool CanSetSlots { get; set; } = false;
    }
}
