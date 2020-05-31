using System;
using System.Collections.Generic;
using System.Text;

namespace AthsEssGymBook.Shared
{
    public class UserInfo0
    {
        public int Id { get; set; }
        //public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public bool HasAccessCard { get; set; } = false;
        public bool IsCoach { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
    }
    public class UserInfo
    {
        public int Id { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public Dictionary<string, string> ExposedClaims { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public bool HasAccessCard { get; set; } = false;
        public bool IsCoach { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
    }

    public class UserInfo2
    {
        public int Id { get; set; }
        //public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public bool HasAccessCard { get; set; } = false;
        public bool IsCoach { get; set; } = false;
        public bool IsAdmin { get; set; } = false;

        public UserInfo2()
        {

        }
    }
}
