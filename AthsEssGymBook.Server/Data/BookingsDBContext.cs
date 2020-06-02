using AthsEssGymBook.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthsEssGymBook.Shared;

namespace AthsEssGymBook.Server.Data
{
    public class BookingsDBContext : DbContext
    {
        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<BookingInfo> BookingInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=data.db");

        }
    }
}
