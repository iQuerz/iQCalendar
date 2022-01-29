using ServerAPI.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public int AccountID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool Recurring { get; set; }
        public RecurringType RecurringType { get; set; }
        public string Color { get; set; }
        public string Notifications { get; set; }
    }
}
