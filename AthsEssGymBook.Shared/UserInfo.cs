using System;
using System.Collections.Generic;
using System.Text;

namespace AthsEssGymBook.Shared
{
    public class UserInfo
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public Dictionary<string, string> ExposedClaims { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
