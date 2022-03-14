using EcommerceDemoWeb.Models;
using EcommerceDemoWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace EcommerceDemoWeb.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> ProductList = _db.Product.ToList();

            return View(ProductList);
        }

        public IActionResult Details(int productId)
        {

            var product = _db.Product.FirstOrDefault(u => u.Id == productId);
            ShoppingCart cartObj = new()
            {
                Count = 1,
                ProductId = productId,
                Product = product,
                Price = product.ListPrice,
            };
            
            return View(cartObj);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            ShoppingCart cartFromDb = _db.ShoppingCarts.FirstOrDefault(
            u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);

            if (cartFromDb == null)
            {
               
                _db.ShoppingCarts.Add(shoppingCart); 
            }
            else
            {
              
                cartFromDb.Count+=shoppingCart.Count;
            }
            shoppingCart.Product = _db.Product.FirstOrDefault(u => u.Id == shoppingCart.ProductId);
            _db.SaveChanges();


            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}