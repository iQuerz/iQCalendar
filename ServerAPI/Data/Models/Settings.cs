using System;
using System.ComponentModel.DataAnnotations;

namespace ServerAPI.Data.Models
{
    public class Settings
    {
        [Key]
        public string ServerName { get; set; }
        public string ServerPassword { get; set; }
        public int Port { get; set; }
        public string HostEmailUsername { get; set; }
        public string HostEmailPassword { get; set; }
        public int NotificationTime { get; set; }
    }
}
