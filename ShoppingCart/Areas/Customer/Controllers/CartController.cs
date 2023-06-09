using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommonHelper;
using DataAccessLayer.Infrastructure.IRepository;
using Models;
using Models.ViewModels;
using Stripe.Checkout;
using System.Security.Claims;

namespace ShoppingCart.Areas.Customer.Controllers/*ham logon ko functionality samajhni hoti hai ki vo cheez kaise kaam kar rahi hai, concept ko clear kar ke aap logon ko kaam karna hai*/
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private IUnitOfWork _uw;
        public CartVM vm { get; set; }

        public CartController(IUnitOfWork uw)
        {
            _uw = uw;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;  //  yaha vo user ham nikalenge jo login ho or apne cart ko chk kar rahe ho
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            vm = new CartVM()
            {
                ListOfCart = _uw.Cart.GetAll(x => x.ApplicationUserId == claims.Value, includeProperties: "Product"), // Expression<Func<T, bool>> <- usi user ki hamein cart details chaiye isliye hamein yahan predicate dalna pada eith nullable, predicate se hamein loged in user ka pata chal jaega
                OrderHeader = new Models.OrderHeader()
            };            

            foreach (var item in vm.ListOfCart)
            {
                vm.OrderHeader.OrderTotal += (item.Product.Price * item.Count);
            }
            return View(vm);  //  select press cnt+r+r for renaming itemList from vm
        }

        [HttpGet]
        public IActionResult Summary() // by this method we show you payment summary page
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            vm = new CartVM()
            {
                ListOfCart = _uw.Cart.GetAll(x => x.ApplicationUserId == claims.Value, includeProperties: "Product"), // Expression<Func<T, bool>> <- usi user ki hamein cart details chaiye isliye hamein yahan predicate dalna pada eith nullable, predicate se hamein loged in user ka pata chal jaega
                OrderHeader = new Models.OrderHeader()
            };
            vm.OrderHeader.ApplicationUser = _uw.ApplicationUser.GetT(x => x.Id == claims.Value); // GetT is used also for getting a single user
            vm.OrderHeader.Name = vm.OrderHeader.ApplicationUser.Name; // Summary page par use karne ke liye ApplicationUser ka data
            vm.OrderHeader.Phone = vm.OrderHeader.ApplicationUser.PhoneNumber;
            vm.OrderHeader.Address = vm.OrderHeader.ApplicationUser.Address;
            vm.OrderHeader.State = vm.OrderHeader.ApplicationUser.State;
            vm.OrderHeader.City = vm.OrderHeader.ApplicationUser.City;
            vm.OrderHeader.PostalCode = vm.OrderHeader.ApplicationUser.PinCode;

            foreach (var item in vm.ListOfCart)
            {
                vm.OrderHeader.OrderTotal += (item.Product.Price * item.Count);
            }
            return View(vm);
        }

        [HttpPost]
         // [ValidateAntiForgeryToken] <- AntiForgeryToken form ko validate nahi karne de rha tha isliye hamne ye hata diya
        public IActionResult Summary(CartVM vm)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            vm.ListOfCart = _uw.Cart.GetAll(x => x.ApplicationUserId == claims.Value, includeProperties: "Product");
            vm.OrderHeader.OrderStatus = OrderStatus.StatusPending; // we set here our StatusPending flag for showing OrderStatus pending to user
            vm.OrderHeader.PaymentStatus = PaymentStatus.StatusPending;
            vm.OrderHeader.DateOfOrder = DateTime.Now;
            vm.OrderHeader.ApplicationUserId = claims.Value;

            foreach (var item in vm.ListOfCart)
            {
                vm.OrderHeader.OrderTotal += (item.Product.Price * item.Count);
            }
            _uw.OrderHeader.Add(vm.OrderHeader);
            _uw.Save();
            foreach (var item in vm.ListOfCart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductId = item.ProductId,
                    OrderHeaderId = vm.OrderHeader.Id,
                    Count = item.Count,
                    Price = item.Product.Price
                };
                _uw.OrderDetail.Add(orderDetail);
                _uw.Save();
            }

             // Here we use our Stripe Checkout List for Payment Options
             // Stripe ka checkout list hamne copy paste kia hai, har stripe payment method ek session create karta hai
             // ham apne project ko run kar ke domain information get islie kar lete hai, domain information get karna isliye zaruri hai taki payment success hone ke baad ham dubaara se apne success wale page par aa sake after order confirmation or cancelation of order
            var domain = "https://localhost:7115/";
            var options = new SessionCreateOptions  //  hamara har ek Stripe payment method ek session create karta hai to hamein yahan Checkout use karni hai na ki Billing
            {
                LineItems = new List<SessionLineItemOptions>(), // cart ke ander hamare jitne items hote hai vo sari informations LineItems ki hoti hai. LineItems is equal to CartItems
                Mode = "payment",                
                SuccessUrl = domain + $"customer/cart/ordersuceess?id={vm.OrderHeader.Id}", // rdersuceess <- action name
                CancelUrl = domain + $"customer/cart/Index",
            };

            foreach (var item in vm.ListOfCart)
            { // After making session by stripe generate a session id and paymentintend id after payment done of order, for updateing these id's we make a new method in IOrderHeaderRepository interface
                var lineItemsOptions = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Product.Price * 100), // UnitAmount means our price value
                        Currency = "INR",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name                            
                        },
                    },
                    Quantity = item.Count,
                };
                options.LineItems.Add(lineItemsOptions);
            }

            var service = new SessionService(); // Session <- har stripe method ek session banata hai, jab session ban jata hai tab ek session id and paymentitent id banti hai jab hamari payment clear ho jati hai <- to isse hamne yahan apni payment ko update kia hai using PaymetStatus method
            Session session = service.Create(options);
            _uw.OrderHeader.PaymetStatus(vm.OrderHeader.Id, session.Id, session.PaymentIntentId);
            _uw.Save();

            _uw.Cart.DeleteRange(vm.ListOfCart); // after adding all cart items in OrderDetails remove the cart
            _uw.Save();

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
            
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ordersuceess(int id) // is method ke throw ham apna payment ka status change karwaenge jab hamari payment success ho jaegi
        {
            var orderHeader = _uw.OrderHeader.GetT(x => x.Id == id);
            var service = new SessionService();
            Session session = service.Get(orderHeader.SessionId); // payment successfull hone ke baad hamein service se SessionId udhani hoti hai payment status janne ke liye
            if (session.PaymentStatus.ToLower() == "paid") // agar order paid hai to payment ka ham pata laga sakte hai session se, these provided by stripe bydefault
            {
                _uw.OrderHeader.UpdateStatus(id, OrderStatus.StatusApproved, PaymentStatus.StatusApproved); // agar order payed hai to payment ka status update ho jana chaiye
            }
            List<Cart> cart = _uw.Cart.GetAll(x => x.ApplicationUserId == orderHeader.ApplicationUserId).ToList(); // yahan hamare pass current login users ApplicationUserId OrderHeader se nikal rahe hai, claim values nahi hai hamare pass yahan
            _uw.Cart.DeleteRange(cart);
            _uw.Save();
            return View(id); // ye id ham Order Id Show karane ke liye use me lenge Order ka
        }

        public IActionResult plus(int id) // id <- cart id
        {
            var cart = _uw.Cart.GetT(x => x.Id == id); // GetT methos is used for getting a single details/Tuple
            _uw.Cart.IncreamentCartItem(cart, 1); // cart, 1 <- in IncreamentCartItem method we passed cart with counter value 1(increament value), as a increament counter ham log as a ddl bhi use kar sakte hai
            _uw.Save();
            return RedirectToAction(nameof(Index)); // nameof capture a method name
        }

        public IActionResult minus(int id)
        {
            var cart = _uw.Cart.GetT(x => x.Id == id);
            if (cart.Count<=1) // agar hamare pass car ki value 1 ya 1 se kam aegi to ham delete karenge
            {
                _uw.Cart.Delete(cart);
                var count = _uw.Cart.GetAll(x => x.ApplicationUserId == cart.ApplicationUserId).ToList().Count-1;
                HttpContext.Session.SetInt32("SessionCart", count);
            }
            else // nahi to ham cart me value add karenge
            {
                _uw.Cart.DecreamentCartItem(cart, 1);
            }

            _uw.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult delete(int id)
        {
            var cart = _uw.Cart.GetT(x => x.Id == id);
            _uw.Cart.Delete(cart);
            _uw.Save();
            var count = _uw.Cart.GetAll(x => x.ApplicationUserId == cart.ApplicationUserId).ToList().Count; // here update our session value after del record
            HttpContext.Session.SetInt32("SessionCart", count); // cart count value set here
            return RedirectToAction(nameof(Index));
        }
    }
}

 // Stripe is used for testing and payments -> https://dashboard.stripe.com/
 // Steps:- signin after register new business,Developer,ApiKey,copy pk,sk and paste in appsetting
 // How to get any info about stripe -> dashboard,help,developer docs,search(stripe checkout,accept a payment(https://stripe.com/docs/payments/accept-a-payment?integration=checkout)),copy code and pase in method cart post
 // For testing stripe -> https://stripe.com/docs/testing, enter card no. etc., in dashboard we can see all our payment details in stripe
