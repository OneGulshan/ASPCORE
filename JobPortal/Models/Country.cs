using System.ComponentModel.DataAnnotations;

namespace JobPortal.Models
{
    public class Country
    {
        [Key]
        public int CId { get; set; }
        public string Name { get; set; }
    }
}
