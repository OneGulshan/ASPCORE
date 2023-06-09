using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    public class Employee
    {
        [Key]
        public int EId { get; set; }
        public string Name { get; set; }
        public Gender? Gender { get; set; }
        public int JobProfileId { get; set; }
        public int SkillsId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long Mobno { get; set; }
        public int Age { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Comment { get; set; }
        [System.ComponentModel.DefaultValue(false)]
        public bool Status { get; set; }
    }
    public enum Gender
    {
        Male = 1,
        Female = 2,
        Others = 3
    }
}
