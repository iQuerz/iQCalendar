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
        private int month;

        public Account Account { get; set; }
        public List<Event> Events { get; set; }

        public int CurrentYear { get; set; }
        public int CurrentMonth
        {
            get => month;
            set
            {
                if (value == 13) { month = 1; CurrentYear++; }
                else if (value == 0) { month = 12; CurrentYear--; }
                else month = value;
            }
        }

        public Manager()
        {
            Account = new Account();
            Events = new List<Event>();
            CurrentYear = DateTime.Now.Year;
            month = DateTime.Now.Month;
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

            Account = new Account
            {
                AccountID = 1,
                Name = "Ralex",
                ClientPassword = "client",
                AdminPassword = "admin"
            };
        }
    }
}
