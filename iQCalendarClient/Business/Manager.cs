using iQCalendarClient.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iQCalendarClient.Business
{
    class Manager
    {
        public List<Account> Accounts { get; set; }
        public List<Event> Events { get; set; }
        public Manager()
        {
            Accounts = new List<Account>();
            Events = new List<Event>();
        }
        public void loadTestData()
        {
            Events.Add(new Event
            {
                EventID = 1,
                AccountID = 1,
                Name = "Clio Registracija",
                Description = "idi registruj clia",
                Date = new DateTime(2022, 2, 20),
                Color = "AliceBlue"
            });
            Events.Add(new Event
            {
                EventID = 2,
                AccountID = 1,
                Name = "Caddy Registracija",
                Description = "idi registruj caddyja",
                Date = new DateTime(2022, 2, 12),
                Color = "Green"
            });
            Events.Add(new Event
            {
                EventID = 3,
                AccountID = 1,
                Name = "Dan Zaljubljenih",
                Description = "kupi zenama cvece",
                Date = new DateTime(2022, 2, 14),
                Color = "Red"
            });

            Accounts.Add(new Account
            {
                AccountID = 1,
                Name = "Ralex",
                ClientPassword = "client",
                AdminPassword = "admin"
            });
        }
    }
}
