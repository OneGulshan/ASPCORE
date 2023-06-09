using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public static class OrderStatus//hamne kuch status OrderHeader ke ander banae the like orderStatus, paymentStatus etc unke liye hamein flag value banani padegi isliye hamne OrderStatus class ko ek static class bana liya
    {
        public const string StatusPending = "Pending"; //Status ham text ke throw na access kar ke ham properties ke throw access karenge isliye hamne ye static class banai hai basically, by using flags
        public const string StatusRefund = "Refunded";
        public const string StatusApproved = "Approved";
        public const string StatusCancelled = "Cancelled";
        public const string StatusInProccess = "UnderProcessing";
        public const string StatusShipped = "Shipped";
    }
}
