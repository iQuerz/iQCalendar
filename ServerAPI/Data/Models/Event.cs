using System;
using System.ComponentModel.DataAnnotations;

using ServerAPI.Models.Types;

namespace ServerAPI.Data.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        public int AccountID { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public RecurringType RecurringType { get; set; }

        [Required]
        public bool Finished { get; set; }


        public string Color { get; set; }

        [MaxLength(50)]
        public string Notifications { get; set; }
    }
}
