using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommonHelper;
using DataAccessLayer.Infrastructure.IRepository;
using Models;
using Models.ViewModels;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace ShoppingCart.Areas.Admin.Controllers
{//ham apne order ki info yahin se get/set karenge ab se
    [Area("Admin")]
    [Authorize] // Authorize se hamara credentials ho gaya means hamara yaha Authorized ho gaya ab sirf loged in user hi hoga, or loged in user hoga to vo kisi na kisi role me bhi zarur hoga vo anonimus role me nahi ho sakta    
    public class OrderController : Controller // API CALL ke throw ham is controller me order header get karenge
    {
        private IUnitOfWork _uw;

        public OrderController(IUnitOfWork uw)
        {
            _uw = uw;
        }

        #region APICALL
        public IActionResult AllOrders(string status)
        {
            IEnumerable<OrderHeader> orderHeaders;
            //orderHeaders = _uw.OrderHeader.GetAll(includeProperties: "ApplicationUser");

            if (User.IsInRole("Admin") || User.IsInRole("Employee")) // IsInRole means konse Role me hai // IsInRole means konse role me hai, agar in 2 role me ho to hamein sare order dikhai de, perticular user ka order dikhai na de // Admin hi sirf saare Orders get kar sakta hai
            {
                orderHeaders = _uw.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity; // yahan hamare pass perticular user ki order se related saari info hai
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                orderHeaders = _uw.OrderHeader.GetAll(x => x.ApplicationUserId == claims.Value);//yaha ham role wise user get kar rhe hain
            }

            switch (status)
            {
                case "pending":
                    orderHeaders = orderHeaders.Where(x => x.PaymentStatus == PaymentStatus.StatusPending);
                    break;
                case "approved":
                    orderHeaders = orderHeaders.Where(x => x.PaymentStatus == PaymentStatus.StatusApproved);
                    break;
                case "underprocess":
                    orderHeaders = orderHeaders.Where(x => x.OrderStatus == OrderStatus.StatusInProccess);
                    break;
                case "shipped":
                    orderHeaders = orderHeaders.Where(x => x.OrderStatus == OrderStatus.StatusShipped);
                    break;

                default:
                    break;
            }

            return Json(new { data = orderHeaders });
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OrderDetails(int id) // for showing Orders Status wize to user and admin by lable(user) and text(admin)
        {
            OrderVM orderVM = new OrderVM() // yahan ham wahi order shiped karenge jisko Admin dekh paega
            {
                orderHeader = _uw.OrderHeader.GetT(x => x.Id == id,
                includeProperties: "ApplicationUser"),
                OrderDetails = _uw.OrderDetail.GetAll(x => x.Id == id, includeProperties: "Product")
            };

            return View(orderVM);
        }

        [Authorize(Roles = WebSiteRole.Role_Admin + "," + WebSiteRole.Role_Employee)]
        [HttpPost]
        public IActionResult OrderDetails(OrderVM vm)
        {
            var orderHeader = _uw.OrderHeader.GetT(x => x.Id == vm.orderHeader.Id);
            orderHeader.Name = vm.orderHeader.Name;
            orderHeader.Phone = vm.orderHeader.Phone;
            orderHeader.Address = vm.orderHeader.Address;
            orderHeader.City = vm.orderHeader.City;
            orderHeader.State = vm.orderHeader.State;
            orderHeader.PostalCode = vm.orderHeader.PostalCode;
            if (vm.orderHeader.Carrier != null)//bhot bar aisa hota hai ki order detail dalte samye hi information daal di jaati hai admin ke dwara carriers or details ki jise carries bhi update ho jate hai saath me or tracking no.s bhi
            {
                orderHeader.Carrier = orderHeader.Carrier;
            }
            if (vm.orderHeader.TrackingNumber != null)
            {
                orderHeader.TrackingNumber = orderHeader.TrackingNumber;

            }
            _uw.OrderHeader.Update(orderHeader);//update info ke baad jo user ko info dikhni hai vo dhigi bakki nahi dikhegi
            _uw.Save();
            TempData["success"] = "Info Updated";//update hone ke baad msg show karane ke liye
            return RedirectToAction("OrderDetails", "Order", new { id = vm.orderHeader.Id });//jyada hard to TK bata hi nahi rha me jise aap kar na saken
        } //In Process btn se under Process me chala jaega Order Status tak ham ise ship kar sakte hain

        [Authorize(Roles = WebSiteRole.Role_Admin + "," + WebSiteRole.Role_Employee)]
        public IActionResult InProcess(OrderVM vm) // Here Change our Order Status by StatusInProccess Order  // InProcess karne ke liye hamein order ke kuch status change karne honge
        {//Order ka status under process me chala jae tab aap use ship kar sakte hain after approving order showed you these 3 buttons simple
            //order status ka matlab ye hota hai hamare pass ki ham kuch basic info cng kar sake
            //ham yahan update status call karenge isme ham order header id ke throw inprocess change kar denge
            //
            _uw.OrderHeader.UpdateStatus(vm.orderHeader.Id, OrderStatus.StatusInProccess); // Here we change Status of Orders helpin of a Model OrderStatus
            _uw.Save();
            TempData["success"] = "Order Status Updated-Inprocess";
            return RedirectToAction("OrderDetails", "Order", new { id = vm.orderHeader.Id });
        }
        
        [Authorize(Roles = WebSiteRole.Role_Admin + "," + WebSiteRole.Role_Employee)]
        public IActionResult Shipped(OrderVM vm) // Here Change our Order Status by Shipped Order
        {
            var orderHeader = _uw.OrderHeader.GetT(x => x.Id == vm.orderHeader.Id);
            orderHeader.Carrier = vm.orderHeader.Carrier;//after providig Carrier,TrackingNumber we can shipped our order
            orderHeader.TrackingNumber = vm.orderHeader.TrackingNumber;
            orderHeader.OrderStatus = OrderStatus.StatusShipped;
            orderHeader.DateOfShipping = DateTime.Now; // after shipped parcel date time change by now

            _uw.OrderHeader.Update(orderHeader);
            _uw.Save();
            TempData["success"] = "Order Status Updated-Shipped";
            return RedirectToAction("OrderDetails", "Order", new { id = vm.orderHeader.Id });
        }

        [Authorize(Roles = WebSiteRole.Role_Admin + "," + WebSiteRole.Role_Employee)]
        public IActionResult CancelOrder(OrderVM vm)
        {
            var orderHeader = _uw.OrderHeader.GetT(x => x.Id == vm.orderHeader.Id);
            if (orderHeader.PaymentStatus == PaymentStatus.StatusApproved)
            {
                var refund = new RefundCreateOptions
                {                    
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeader.PaymentIntentId
                };
                var service = new RefundService();                
                Refund Refund = service.Create(refund);
                _uw.OrderHeader.UpdateStatus(orderHeader.Id, OrderStatus.StatusCancelled, OrderStatus.StatusRefund);
            }
            else
            {
                _uw.OrderHeader.UpdateStatus(orderHeader.Id, OrderStatus.StatusCancelled, OrderStatus.StatusCancelled);
            }

            _uw.Save();
            TempData["success"] = "Order Cancelled";
            return RedirectToAction("OrderDetails", "Order", new { id = vm.orderHeader.Id });
        }

        public IActionResult PayNow(OrderVM vm)
        {//In latter section is repeatearion of code ka ham ek alag hi method bana denge in common class me taaki hamein ye code repeate na likhna pade
            var orderHeader = _uw.OrderHeader.GetT(x => x.Id == vm.orderHeader.Id, includeProperties: "ApplicationUser");
            var OrderDetails = _uw.OrderDetail.GetAll(x => x.Id == vm.orderHeader.Id, includeProperties: "Product");

            var domain = "https://localhost:7115/";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain+$"customer/cart/ordersuceess?id={vm.orderHeader.Id}",
                CancelUrl = domain+$"customer/cart/Index",
            };

            foreach (var item in vm.OrderDetails)
            {
                var lineItemsOptions = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Product.Price * 100),
                        Currency = "inr",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name,
                        },
                    },
                    Quantity = item.Count,
                };
                options.LineItems.Add(lineItemsOptions);
            }

            var service = new SessionService();
            Session session = service.Create(options);
            _uw.OrderHeader.PaymetStatus(vm.orderHeader.Id, session.Id, session.PaymentIntentId);
            _uw.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
            return RedirectToAction("Index", "Home");
        }
    }
}