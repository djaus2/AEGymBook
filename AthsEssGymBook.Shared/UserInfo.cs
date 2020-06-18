using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace AthsEssGymBook.Shared
{
    [Table (("Athletes"))]
    public class Athlete
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [Column ("UserName")]
        //public bool IsAuthenticated { get; set; }
        [JsonPropertyName("UserName")]
        public string UserName { get; set; }

        [Column("Email")]
        [Required]
        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [Column("PhoneNumber")]
        [JsonPropertyName("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Column("HasAccessCard")]
        [JsonPropertyName("HasAccessCard")]
        public bool HasAccessCard { get; set; } = false;

        [Column("IsAdmin")]
        [JsonPropertyName("IsAdmin")]
        public bool IsAdmin { get; set; } = false;

        [Column("IsCoach")]
        [JsonPropertyName("IsCoach")]
        public bool IsCoach { get; set; } = false;

        [Column("CanSetSlots")]
        [JsonPropertyName("CanSetSlots")]
        public bool CanSetSlots { get; set; } = false;

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
        public bool IsAdmin { get; set; } = false;
        public bool IsCoach { get; set; } = false;
        public bool CanSetSlots { get; set; } = false;

    }
}
