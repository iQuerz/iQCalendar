using Microsoft.EntityFrameworkCore;

using ServerAPI.Data.Models;

namespace ServerAPI.Data
{

    public class CalendarContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Settings> Settings { get; set; }

        public CalendarContext(DbContextOptions options)
            :base(options)
        {

        }
    }
}
