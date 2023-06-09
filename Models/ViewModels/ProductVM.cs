using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ProductVM
    {
        public Product? Product { get; set; }/* = new Product();*/ //on controller we initialize this so does,nt req here
        [ValidateNever]//hamare pass kuch parameters sahi nahin hote hai jise hamein validate never karna hoga/hota hai
        public IEnumerable<Product>? Products { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}
