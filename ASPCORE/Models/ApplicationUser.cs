using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } //Identity customization
        public string LastName { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
    }
    public enum Gender
    {
        Male,
        Female,
        others
    }
}
//Identity used for Creadentials/Authorisation
//Enumeration is enum it's a method