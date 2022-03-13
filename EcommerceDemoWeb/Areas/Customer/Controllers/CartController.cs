using EcommerceDemoWeb.Areas.Customer.Models.ViewModels;
using EcommerceDemoWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceDemoWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartVM ShoppingCartVm { get; set; }
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Area("Customer")]
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var aui = _db.ShoppingCarts.ToList();
            var shopcart = _db.ShoppingCarts.ToList().Where(o=> o.ApplicationUserId== claim.Value);
            ShoppingCartVm = new ShoppingCartVM() {
                
                ListCart = _db.ShoppingCarts.ToList().Where(u => u.ApplicationUserId == claim.Value)
            };
            ViewBag.shopcart = shopcart;
            ViewBag.cartItems = ShoppingCartVm;
            return View(ShoppingCartVm);
        }


    }
}
