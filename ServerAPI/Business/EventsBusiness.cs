using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ServerAPI.Data.Models;

namespace ServerAPI.Business
{
    public partial class Logic
    {
        public async Task<List<Event>> GetMonthlyEvents(int accountID, int month, int year)
        {
            if (accountID <= 0)
            {
                var err = new iQError
                {
                    Error = "Invalid ID.",
                    Details = "S01 - Negative ID sent. ID's start with 1 and go only above."
                };
                throw new iQException(err, 400);
            }

            if (month < 1 || month > 12)
            {
                var err = new iQError
                {
                    Error = "Invalid request.",
                    Details = "E01 - Month value is outside of bounds. Make sure the month number is between 1 and 12."
                };
                throw new iQException(err, 400);
            }

            var account = await _context.Accounts.FindAsync(accountID);

            if (account == null)
            {
                var err = new iQError
                {
                    Error = "Account not found.",
                    Details = "A01 - The specified account does not exist in our database. Try with a different one."
                };
                throw new iQException(err, 404);
            }

            //list to be returned
            List<Event> events = new List<Event>();

            //var events = _context.Events.Where(e => e.AccountID == account.AccountID);
            //events = events.Where(e => e.Date.Month == month || )

            foreach(Event e in _context.Events)
            {
                if (e.AccountID != account.AccountID)
                    continue;

                if (e.Date.Month != month)
                    continue;

                if (e.Date.Year != year)
                    continue;

                events.Add(e);
            }

            return events;

        }

        public async Task<Event> CreateEvent(Event @event)
        {
            var e = _context.Events.FirstOrDefault(ev => ev.Name == @event.Name);

            if (e != null)
            {
                var err = new iQError
                {
                    Error = "Event already exists.",
                    Details = "E02 - The database already has an event with the same name. Try giving it a different name."
                };
                throw new iQException(err, 400);
            }

            await _context.Events.AddAsync(@event);
            await _context.SaveChangesAsync();
            return @event;
        }

        public async Task<Event> UpdateEvent(Event @event)
        {
            var e = await _context.Events.FindAsync(@event.EventID);

            if (e == null)
            {
                var err = new iQError
                {
                    Error = "Event not found.",
                    Details = "E03 - The specified event does not exist in our database. Try a different one."
                };
                throw new iQException(err, 404);
            }

            e.AccountID = @event.AccountID;
            e.Name = @event.Name;
            e.Description = @event.Description;
            e.Date = @event.Date;
            e.RecurringType = @event.RecurringType;
            e.Finished = @event.Finished;
            e.Color = @event.Color;
            e.Notifications = @event.Notifications;

            await _context.SaveChangesAsync();

            return e;
        }

        public async Task DeleteEvent(int eventID)
        {

            if (eventID < 0)
            {
                var err = new iQError
                {
                    Error = "Invalid ID.",
                    Details = "S01 - Negative ID sent. ID's start with 1 and go only above."
                };
                throw new iQException(err, 400);
            }

            var e = await _context.Events.FindAsync(eventID);

            if (e == null)
            {
                var err = new iQError
                {
                    Error = "Event not found.",
                    Details = "E03 - The specified event does not exist in our database. Try a different one."
                };
                throw new iQException(err, 404);
            }

            _context.Events.Remove(e);
            await _context.SaveChangesAsync();
        }


        private void getDateTimesRecurringType() { } // sheesh papa idk how to start
    }
}
