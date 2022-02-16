using System.Collections.Generic;
using System.Threading.Tasks;

using ServerAPI.Data.Models;

namespace ServerAPI.Business
{
    public partial class Logic
    {
        public async Task<List<Event>> GetMonthlyEvents(int accountID, int month, int year)
        {

            #region Exceptions
            if (month < 1 || month > 12)
                throw new iQException();

            var account = await _context.Accounts.FindAsync(accountID);

            if (account == null)
                throw new iQException();
            #endregion

            #region Code
            //list to be returned
            List<Event> events = new List<Event>();

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

            #endregion
        }

        public async Task<Event> CreateEvent(Event @event)
        {
            await _context.Events.AddAsync(@event);
            await _context.SaveChangesAsync();
            return @event;
        }

        public async Task<Event> UpdateEvent(Event @event)
        {
            var e = await _context.Events.FindAsync(@event.EventID);

            if (e == null)
                throw new iQException();

            e.AccountID = @event.AccountID;
            e.Name = @event.Name;
            e.Description = @event.Description;
            e.Date = @event.Date;
            e.RecurringType = @event.RecurringType;
            e.IterationsFinished = @event.IterationsFinished;
            e.Color = @event.Color;
            e.Notifications = @event.Notifications;

            await _context.SaveChangesAsync();

            return e;
        }

        public async Task DeleteEvent(int eventID)
        {
            #region Exceptions

            if (eventID < 0)
                throw new iQException("Invalid ID.");

            var e = await _context.Events.FindAsync(eventID);

            if (e == null)
                throw new iQException("Can't find the event.");

            #endregion

            _context.Events.Remove(e);
            await _context.SaveChangesAsync();
        }

    }
}
