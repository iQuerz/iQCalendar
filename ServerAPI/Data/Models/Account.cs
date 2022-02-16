using System.ComponentModel.DataAnnotations;

namespace ServerAPI.Data.Models
{
    public class Account
    {
        [Key]
        public string AccountID { get; set; }
        public string Name { get; set; }
        public string Recipients { get; set; }

        //[Required]
        public string ClientPassword { get; set; }

        //[Required]
        public string AdminPassword { get; set; }
    }
}
