using System.ComponentModel.DataAnnotations;

namespace ServerAPI.Data.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        public string Recipients { get; set; }

        [Required]
        [MinLength(5)]
        public string ClientPassword { get; set; }

        [Required]
        [MinLength(5)]
        public string AdminPassword { get; set; }

    }
}
