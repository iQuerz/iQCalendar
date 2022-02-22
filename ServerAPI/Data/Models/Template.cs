using System.ComponentModel.DataAnnotations;

using ServerAPI.Models.Types;

namespace ServerAPI.Data.Models
{
    public class Template
    {

        [Key]
        public int TemplateID { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string TemplateName { get; set; }


        public RecurringType RecurringType { get; set; }


        public string Color { get; set; }

        [MaxLength(50)]
        public string Notifications { get; set; }

    }
}
