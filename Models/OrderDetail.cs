using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrderDetail
    {
        public int Id { get; set; }//here OrderHeaderId and ProductId is required, Id is OrderId here
        [Required]
        public int OrderHeaderId { get; set; }
        [ValidateNever]
        public OrderHeader? OrderHeader { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ValidateNever]
        public Product? Product { get; set; }
        public double Price { get; set; }//Price/PaymentPrice for GrantTotal
        public int Count { get; set; }//No. of Count Items
    }
}
//hamne ek summary page banaya hai jiske liye ham Order detail or Order header use kar rahe hai with payment information