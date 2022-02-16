using System.ComponentModel.DataAnnotations;

namespace ServerAPI.Data.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }
        public string Name { get; set; }
        public string Recipients { get; set; }

        //[Required]
        public string ClientPassword { get; set; }

        //[Required]
        public string AdminPassword { get; set; }
    }
}
