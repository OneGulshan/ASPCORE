using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCORE.Models;

namespace ASPCORE.ViewModels
{
    public class UserRoles
    {
        public ApplicationUser AppUser { get; set; }
        public List<SelectListItem> roles { get; set; }
        public List<SelectListItem> claims { get; set; }
    }
}
