using System;

using iQCalendarClient.Business.Models.Types;

namespace iQCalendarClient.Business.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public int AccountID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public RecurringType RecurringType { get; set; }
        public int IterationsFinished { get; set; }
        public string Color { get; set; }
        public string Notifications { get; set; }
    }
}
