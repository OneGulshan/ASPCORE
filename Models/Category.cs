using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [DisplayName("Display Order")]//DisplayName prop used here for gapping between text, by this where we use this prop our DisplayOrder text show -> Display Order in this formate
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;        
    }
}
