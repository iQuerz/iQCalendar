using iQCalendarClient.Business.Models;
using iQCalendarClient.Business.Models.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace iQCalendarClient.Business
{
    class Manager
    {

        #region Fields & Properties
        private int month { get; set; }

        /// <summary>
        /// Represents the iQCalendar Account that Manager is tied to.
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// Represents the list of Events that Manager is currently working with.
        /// </summary>
        public List<Event> Events { get; set; }

        /// <summary>
        /// Represents Client the settings object.
        /// </summary>
        public ClientSettings Settings { get; set; }


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

        private string apiUrl
        {
            get { return $"https://{Settings.ServerIP}:{Settings.ServerPort}/"; }
        }

        #endregion

        /// <summary>
        /// Initializes the Manager with current month and year while loading the client settings.
        /// </summary>
        public Manager()
        {
            Account = new Account();
            Events = new List<Event>();
            CurrentYear = DateTime.Now.Year;
            month = DateTime.Now.Month;
            Settings = new ClientSettings();
            Settings.loadSettings();
        }

        #region Testing

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

        #endregion


        /// <summary>
        /// Compares the <paramref name="date"/> with <see cref="CurrentMonth"/> and <see cref="CurrentYear"/>
        /// </summary>
        /// <param name="date"></param>
        /// <returns>True if <paramref name="date"/> is inside the bounds of above mentioned month and year, otherwise, false.</returns>
        private bool isDateInsideCurrentMonth(DateTime date)
        {
            if (date.Month != CurrentMonth)
                return false;

            if (date.Year != CurrentYear)
                return false;

            return true;
        }

        public async Task loadEventsAsync()
        {
            HttpResponseMessage result;
            using (HttpClient client = new HttpClient())
            {
                var auth = getHashedAuth(Settings.CachedUsername, Settings.CachedPassword);
                client.DefaultRequestHeaders.Add("Authorization", auth);
                result = await client.GetAsync( apiUrl + $"api/Events/{Settings.AccountID}/{CurrentMonth}/{CurrentYear}");
            }

            if (!result.IsSuccessStatusCode)
                throw new Exception(await result.Content.ReadAsStringAsync());

            var content = await result.Content.ReadAsStringAsync();
            var eventsList = JsonConvert.DeserializeObject<List<Event>>(content);

            foreach (var e in eventsList)
                if (e.Date.Month < CurrentMonth || e.Date.Year < CurrentYear)
                    e.Date = getFirstDateOccuranceInsideCurrentMonth(e.Date, e.RecurringType);

            Events = eventsList;
        }

        private string getHashedAuth(string username, string password)
        {
            string auth = $"{username}:{password}"; // international standard format for basic authentication. username:password

            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            auth = Convert.ToBase64String(encoding.GetBytes(auth));

            return $"Basic {auth}";
        }

        private DateTime getFirstDateOccuranceInsideCurrentMonth(DateTime date, RecurringType recurringType)
        {
            if (recurringType == RecurringType.NonRecurring)
                return date;

            while(date.Month != CurrentMonth || date.Year != CurrentYear)
            {
                if (recurringType == RecurringType.Daily)
                    return new DateTime(CurrentYear, CurrentMonth, 1);

                date = getNextDateFromRecurringType(date, recurringType);
            }
            return date;
        }

        /// <summary>
        /// Finds the next date repetition in regards to its RecurringType
        /// </summary>
        /// <param name="date">Date from which to calculate.</param>
        /// <param name="recurringType">RecurringType used for calculation.</param>
        /// <returns>The next date calculated if RecurringType is above 0. Returns the same date otherwise</returns>
        static DateTime getNextDateFromRecurringType(DateTime date, RecurringType recurringType)
        {
            switch (recurringType)
            {
                case RecurringType.Daily:
                    return date.AddDays(1);
                case RecurringType.Weekly:
                    return date.AddDays(7);
                case RecurringType.Monthly:
                    return date.AddMonths(1);
                case RecurringType.Yearly:
                    return date.AddYears(1);
                default:
                    return date;
            }
        }
    }
}
