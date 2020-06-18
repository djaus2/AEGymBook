using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthsEssGymBook.Server.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public bool IsAdmin { get; set; } = false;
        public bool IsCoach { get; set; } = false;
        public bool HasAccessCard { get; set; } = false;
        public bool CanSetSlots { get; set; } = false;
    }
}
