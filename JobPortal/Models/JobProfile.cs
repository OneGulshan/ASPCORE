using System.ComponentModel.DataAnnotations;

namespace JobPortal.Models
{
    public class JobProfile
    {
        [Key]
        public int JPId { get; set; }
        public string Name { get; set; }
    }
}
