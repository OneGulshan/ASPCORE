using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Infrastructure.IRepository;
using Models;
using System.Security.Claims;

namespace ShoppingCart.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _uw;//ham sare products yaha coustomer ko dikhaenge kunki vo sabhi products ko kharid + dekh sakta hai
        public HomeController(ILogger<HomeController> logger, IUnitOfWork uw)
        {
            _logger = logger;
            _uw = uw;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _uw.Product.GetAll(includeProperties: "Category");
            return View(products);
        }
        [HttpGet]
        public IActionResult Details(int? ProductId)
        {
            Cart cart = new Cart() //ab ham Shopping Cart use karenge instead of(ke bajae) Product
            {
                Product = _uw.Product.GetT(x => x.Id == ProductId, includeProperties: "Category"),//Details me hamare paas ek extra field aati hai jaise ki quantity property jise ham count bolte actualy hai, jisse ham count define kar sakte hai product ke
                Count = 1,//default me Quentity hamari 1 set honi chaiye
                //ProductId = (int)Id //Id and ProductId conflict kar rahi thi isliye hamne routing me Product id ka nam change kar ProductId kar liya ab problem solved agar kisi ki id / kuch or conflict kare to sirf change by diff name thats it
                ProductId = (int)ProductId//yahan hamne ProductId 0 set kar di kunki Cart ki id yaha Product ki id le raha tha ab nahi lega
            };
            return View(cart);//here one product get with its categories
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//hamara AntiForgeryToken model ko check karta hai na ki current state ko isliye hamare pass Application User id nahi aai kunki hamari user id model se na aa kar state se aa rahi hai, state means jo code hamne user id ko access karne ke liye likha hai
        [Authorize]//Authorize attribute used for sending current user for login, identity ko call karne ke liye is method par post karne se phele simply
        public IActionResult Details(Cart cart)//Cart <- ye cart hamara ModelState hai isliye User id nahi aai, kunki Model State check karti hai jo form submit ho kar aaya hai na ki hamne jo assign kia hai usse Model State ko koi matlab nahi hota
        {//hamein current logedin user ki ApplicationUser id bhi chaiye hogi jiske liye hamein AntiForgeryToken and Authorize attribute dono chaiye hoga
            if (ModelState.IsValid)//ham apne Cart Model me ApplicationUserId ko ValidateNever kar denge jisse hamari ApplicationUserId Validate nahi hogi or ab ham ModelState me apne ApplicationUserId ko daal sakenge
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity; // here we return our User Identity as a ClaimsIdentity for simply using User Identity
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier); // by NameIdentifier get name of user
                cart.ApplicationUserId = claims.Value;//User ki identity se hamne user ki current user id nikkal li by helping ClaimsIdentity

                var cartItem = _uw.Cart.GetT(x => x.ProductId == cart.ProductId &&
                x.ApplicationUserId == claims.Value);//pura tuple product nikal liya hamne, means full product with all its required details

                if (cartItem == null)//cart null hai to cart ko add kar denge nahi to cart me items ko add kar denge
                {
                    _uw.Cart.Add(cart);
                    HttpContext.Session.SetInt32("SessionCart", _uw.Cart.GetAll(x => x.ApplicationUserId == claims.Value).ToList().Count); // int or string bydefault ham settion me set kar sakte hain, here set key with value pare in session
                }
                else
                {
                    _uw.Cart.IncreamentCartItem(cartItem, cart.Count);
                }
                _uw.Save();
            }
            return RedirectToAction("Index");//isse phele Index method par jaega fir view par jaega
        }
    }
}
