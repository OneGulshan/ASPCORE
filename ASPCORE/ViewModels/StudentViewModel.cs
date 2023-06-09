using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.ViewModel
{
    public class StudentViewModel
    {
        public string Name { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}
