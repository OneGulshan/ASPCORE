using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrderHeader//necessary things we write in Order Header
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public DateTime DateOfOrder { get; set; }
        public DateTime DateOfShipping { get; set; }//for showing shipped msg
        public double OrderTotal { get; set; }
        public string? OrderStatus { get; set; }//OrderStatus may be null because pending me bhot se order daal die jaate hai kunki unki payment information nahi hoti hai because to vo OrderStatus pendding me aate hai isliye hamne nullable isme banaya hai
        public string? PaymentStatus { get; set; }//Order ke ander payment ki bhi detail hoti hai
        public string? TrackingNumber { get; set; }//by TrackingNumber we can find product by TrackingDetails
        public string? Carrier { get; set; }//Carrier means parcel type Carrier like FedEx etc.
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; } // PaymentIntentId used in Stripe
        public DateTime DateOfPayment { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public DateTime DueDate { get; set; }
        [Required]
        public string Phone { get; set; }        
        [Required]
        public string Name{ get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
    }
}
//Payement,Order,Carrier,Total Dates,UserInfo etc details aa jaengi under the OrderHeader
//Order header ke ander Price ka or Total ka bhi jud jata hai


//What is a PaymentIntent?
//The PaymentIntent encapsulates details about the transaction, such as the supported payment methods, the amount to collect, and the desired currency.

//How do you make a Stripe product?
//Log into your Stripe Dashboard and navigate to the Products dashboard. Click on the + Add Product button. Add: Name of the product: products help you track inventory or provisioning.



