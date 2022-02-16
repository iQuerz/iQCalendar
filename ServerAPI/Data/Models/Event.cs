using System;
using System.ComponentModel.DataAnnotations;

using ServerAPI.Models.Types;

namespace ServerAPI.Data.Models
{
    public class Event
    {
        [Key]
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
