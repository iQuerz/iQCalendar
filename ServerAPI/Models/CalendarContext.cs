using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Models
{

    public class CalendarContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        public CalendarContext(DbContextOptions options)
            :base(options)
        {

        }
    }
}
