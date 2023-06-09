using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class OrderVM//OrderVM prepair for making Order Details Page
    {
        [ValidateNever]
        public OrderHeader? orderHeader { get; set; }
        public IEnumerable<OrderDetail>? OrderDetails { get; set; }//1 order ke ander multiple products hote hai, to iski help se ham iska ek OrderDetails page banaenge
    }
}
