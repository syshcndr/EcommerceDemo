using EcommerceDemoWeb.Models.ViewModels;
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
            
           
            ShoppingCartVm = new ShoppingCartVM() {
                
                ListCart = _db.ShoppingCarts.ToList().Where(u => u.ApplicationUserId == claim.Value)
            };
            ShoppingCartVm.ListCart.ToList().ForEach(x => x.Product = _db.Product.FirstOrDefault(u => u.Id == x.ProductId));
            int sum = 0;
            foreach(var item in ShoppingCartVm.ListCart)
            {
                sum = (int)(sum + item.Product.ListPrice) * item.Count;
            }
            ViewBag.CartSum = sum;

            return View(ShoppingCartVm);
        }

        public IActionResult Increase(int cartId)
        {
            var cart = _db.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            cart.Count = cart.Count+1;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Decrease(int cartId)
        {
            var cart = _db.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            cart.Count = cart.Count -1;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int cartId)
        {
            var cart = _db.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            _db.ShoppingCarts.Remove(cart);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
