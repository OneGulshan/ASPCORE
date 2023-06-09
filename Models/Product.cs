using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }//yahan EF core automatically sence kar lega ki agar hamne yahan Navigation ke liye Category likha hai to EF core bydefault yahan CategoryId ko foriegn key bana dega/bana deta hai <- means jab model me navigation prop use me ki jaati hai or fir model me uski id banai jati hai to vo automatically foreign key me convert ho jati hai by helping of EF core
        [ValidateNever]//Navigate prop we does,nt required to validate, always make this type of prop [ValidateNever]
        public Category Category { get; set; }
    }
}
