using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Cart//this model is created for getting extra field quantity of Product
    {
        public int Id { get; set; }
        public int ProductId { get; set; }//Cart me ProductId add hoti hai for Product navigation key
        [ValidateNever] // Navigation Properties ko hamein validate karne ki zarurat nahi hoti baad me ye error generate karti hai isliye ise ham never validate kar dete hain
        public Product Product { get; set; }
        [ValidateNever]
        public string ApplicationUserId { get; set; }//perticular cart ApplicationUserId ke basis par hota hai
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public int Count { get; set; }
    }
}
//So, this error description says that an object that is being called to get or set its value has no reference.
//This means that you are trying to access an object that was not instantiated.