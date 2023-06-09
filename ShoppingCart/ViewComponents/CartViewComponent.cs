using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Infrastructure.IRepository;
using System.Security.Claims;

namespace ShoppingCart.ViewComponents
{
    public class CartViewComponent: ViewComponent//here for session cart value we making ViewComponent not use if else condition taki database se logout/login par sono condition me cart ki value chk ho
    {
        private readonly IUnitOfWork _uw; // isse ham apne cart ke items dekhenge

        public CartViewComponent(IUnitOfWork uw)
        {
            _uw = uw;
        }
        public async Task<IViewComponentResult> InvokeAsync() // jab-2 ham apni Layout file par jaenge tab-2 hamara ye method call hoga
        {
            var claimIdentity = (ClaimsIdentity)User.Identity; // ClaimsIdentity <- for Current user info getting
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims != null) // claims is user login here
            {
                if (HttpContext.Session.GetInt32("SessionCart")!=null) // jab session cart ki val null nahi hogi to ye set ho jae nahi to else wala
                {
                    return View(HttpContext.Session.GetInt32("SessionCart"));
                }
                else
                {
                    HttpContext.Session.SetInt32("SessionCart", _uw.
                        Cart.GetAll(x => x.ApplicationUserId == claims.Value).ToList().Count);
                    return View(HttpContext.Session.GetInt32("SessionCart"));
                }
            }
            else // agar user login na ho to return me hamare pass 0 aa jana chaiye
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
