using System;
using System.ComponentModel.DataAnnotations;

namespace ServerAPI.Data.Models
{
    public class Settings
    {
        [Key]
        [MinLength(4)]
        public string ServerName { get; set; }

        [Required]
        [MinLength(5)]
        public string ServerPassword { get; set; }

        [Required]
        [Range(5000,9000)]
        public int Port { get; set; }

        [Required]
        [EmailAddress]
        public string HostEmailUsername { get; set; }

        [Required]
        [MinLength(5)]
        public string HostEmailPassword { get; set; }

        [Required]
        [Range(0, 23)]
        public int NotificationTime { get; set; }

    }
}
