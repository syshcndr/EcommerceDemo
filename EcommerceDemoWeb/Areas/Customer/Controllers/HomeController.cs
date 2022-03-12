using EcommerceDemoWeb.Areas.Admin.Models;
using EcommerceDemoWeb.Areas.Customer.Models;
using EcommerceDemoWeb.Data;
using EcommerceDemoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            ShoppingCart cartObj = new()
            {
                Count = 1,
                Product = _db.Product.FirstOrDefault(u=> u.Id== productId),

            };
            
        

            return View(cartObj);
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