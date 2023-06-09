using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ApplicationUser : IdentityUser // here we customize our Identity class using ApplicationUser class for adding our customize features in IdentityUser simple, isse AspNetUsers table me hamari ye prop add ho jaegi or ham fir in prop ko is tbl me use kar sakenge
    {
        [Required]
        public string Name { get; set; }        
        public string? Address { get; set; } // Coustomer if does,nt purchase anything then these details of customer does,nt mandatory so in this case we maked these properties nullable
        public string? City { get; set; }        
        public string? State { get; set; }        
        public string? PinCode { get; set; }                
    }
}
