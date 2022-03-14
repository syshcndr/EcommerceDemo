using EcommerceDemoWeb.Models.ViewModels;
using EcommerceDemoWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EcommerceDemoWeb.Models;
using EcommerceDemoWeb.Utility;

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


            ShoppingCartVm = new ShoppingCartVM()
            {

                ListCart = _db.ShoppingCarts.ToList().Where(u => u.ApplicationUserId == claim.Value),
                OrderHeader = new()
            };
            ShoppingCartVm.ListCart.ToList().ForEach(x => x.Product = _db.Product.FirstOrDefault(u => u.Id == x.ProductId));
            double sum = 0;
            foreach (var item in ShoppingCartVm.ListCart)
            {
                sum = (sum + item.Product.ListPrice) * (double)item.Count;
            }
            ViewBag.CartSum = sum;
            ShoppingCartVm.OrderHeader.OrderTotal = sum;
            return View(ShoppingCartVm);
        }

        public IActionResult Increase(int cartId)
        {
            var cart = _db.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            cart.Count = cart.Count + 1;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Decrease(int cartId)
        {
            var cart = _db.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            if (cart.Count <= 1)
            {
                _db.ShoppingCarts.Remove(cart);
            }
            else
            {
                cart.Count = cart.Count - 1;
            }

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
        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            ShoppingCartVm = new ShoppingCartVM()
            {

                ListCart = _db.ShoppingCarts.ToList().Where(u => u.ApplicationUserId == claim.Value),
                OrderHeader = new()
            };

            ShoppingCartVm.OrderHeader.ApplicationUser = _db.ApplicationUsers.FirstOrDefault(u => u.Id == claim.Value);
            ShoppingCartVm.OrderHeader.Name = ShoppingCartVm.OrderHeader.ApplicationUser.Name;
            ShoppingCartVm.OrderHeader.PhoneNumber = ShoppingCartVm.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVm.OrderHeader.StreetAddress = ShoppingCartVm.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartVm.OrderHeader.City = ShoppingCartVm.OrderHeader.ApplicationUser.City;
            ShoppingCartVm.OrderHeader.State = ShoppingCartVm.OrderHeader.ApplicationUser.State;
            ShoppingCartVm.OrderHeader.PinCode = ShoppingCartVm.OrderHeader.ApplicationUser.PinCode;

            ShoppingCartVm.ListCart.ToList().ForEach(x => x.Product = _db.Product.FirstOrDefault(u => u.Id == x.ProductId));
            double sum = 0;
            foreach (var item in ShoppingCartVm.ListCart)
            {
                sum = sum+ (item.Product.ListPrice * item.Count);
            }
            ShoppingCartVm.OrderHeader.OrderTotal = sum;
    
            return View(ShoppingCartVm);
        }

        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPost(ShoppingCartVM ShoppingCartVm)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVm.ListCart = _db.ShoppingCarts.Where(u => u.ApplicationUserId == claim.Value);

            ShoppingCartVm.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            ShoppingCartVm.OrderHeader.OrderStatus = SD.StatusPending;
            ShoppingCartVm.OrderHeader.OrderDate = System.DateTime.Now;
            ShoppingCartVm.OrderHeader.ApplicationUserId = claim.Value;

            
            double sum = 0;
            foreach (var item in ShoppingCartVm.ListCart)
            {
                item.Product = _db.Product.FirstOrDefault(u => u.Id == item.ProductId);
                sum = sum + (item.Product.ListPrice * item.Count);
            }
            ShoppingCartVm.OrderHeader.OrderTotal = sum;

            _db.OrderHeader.Add(ShoppingCartVm.OrderHeader);
            _db.SaveChanges();

            
            foreach (var cart in ShoppingCartVm.ListCart)
            {
                cart.Product = _db.Product.FirstOrDefault(u => u.Id == cart.ProductId);
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderId = ShoppingCartVm.OrderHeader.Id,
                    Count = cart.Count,
                    Price = cart.Product.ListPrice,
                };
                _db.OrderDetail.Add(orderDetail);
            }
            _db.SaveChanges();



            _db.ShoppingCarts.RemoveRange(ShoppingCartVm.ListCart);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}
