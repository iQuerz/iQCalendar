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

        /// <summary>
        /// Represents the iQCalendar Account that Manager is tied to.
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// Represents the list of Events that Manager is currently working with.
        /// </summary>
        public List<Event> Events { get; set; }

        /// <summary>
        /// Represents the year that Manager is currently working with.
        /// </summary>
        public int CurrentYear { get; set; }

        /// <summary>
        /// Represents the month that Manager is currently working with.
        /// </summary>
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

        /// <summary>
        /// Initializes the Manager with current month and year.
        /// </summary>
        public Manager()
        {
            Account = new Account();
            Events = new List<Event>();
            CurrentYear = DateTime.Now.Year;
            month = DateTime.Now.Month;
        }

        /// <summary>
        /// Loads a few static events and an account.
        /// Delete this data before ineracting with the database using <seealso cref="deleteData()"/>
        /// </summary>
        public void loadTestData()
        {
            Events.Add(new Event
            {
                EventID = 1,
                AccountID = 1,
                Name = "Clio Registracija",
                Description = "idi registruj clia",
                Date = DateTime.Now.AddDays(3),
                Color = "Yellow",
                Finished = false,
                RecurringType = Models.Types.RecurringType.Weekly
            });
            Events.Add(new Event
            {
                EventID = 2,
                AccountID = 1,
                Name = "Caddy Registracija",
                Description = "idi registruj caddyja",
                Date = DateTime.Now.AddDays(5),
                Color = "Green",
                Finished = false,
                RecurringType = Models.Types.RecurringType.NonRecurring
            });
            Events.Add(new Event
            {
                EventID = 3,
                AccountID = 1,
                Name = "Dan Zaljubljenih",
                Description = "kupi zenama cvece",
                Date = DateTime.Now.AddDays(12),
                Color = "Red",
                Finished = true,
                RecurringType = Models.Types.RecurringType.Monthly
            });

            Account = new Account
            {
                AccountID = 1,
                Name = "Ralex",
                ClientPassword = "client",
                AdminPassword = "admin"
            };
        }

        /// <summary>
        /// Deletes the Account and the Events list inside the Manager.
        /// </summary>
        public void deleteData()
        {
            Events.Clear();
            Account = new Account();
        }


    }
}
