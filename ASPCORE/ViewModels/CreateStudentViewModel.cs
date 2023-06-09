using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.ViewModels
{
    public class CreateStudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Enrolled { get; set; }
        public IList<SelectListItem> Courses { get; set; }//SelectListItem is a class which have three types of properties Text,Value and Selected or Unselected(Selected boolean property)
    }
}
