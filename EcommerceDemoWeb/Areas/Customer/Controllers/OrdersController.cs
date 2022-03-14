using EcommerceDemoWeb.Data;
using EcommerceDemoWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceDemoWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrdersController : Controller
    {
        
            private readonly ApplicationDbContext _db;

            public OrdersController(ApplicationDbContext db)
            {
                _db = db;
            }
            public IActionResult Index()
            {
                IEnumerable<OrderHeader> orderHeaders = _db.OrderHeader.ToList();
                return View(orderHeaders);
            }

            public IActionResult Edit(int? id)
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var categoryFromDb = _db.OrderHeader.Find(id);
                if (categoryFromDb == null)
                {
                    return NotFound();
                }
                return View(categoryFromDb);
            }

            [HttpPost]
            public IActionResult Edit(OrderHeader obj)
            {
                var header = _db.OrderHeader.FirstOrDefault(u => u.Id == obj.Id);
                header.OrderStatus = obj.OrderStatus;

                _db.SaveChanges();
                return RedirectToAction("Index");

            }



            [HttpPost]
            public IActionResult DeletePost(int? id)
            {
               
            var header = _db.OrderHeader.FirstOrDefault(u => u.Id == id);
            
            
                header.OrderStatus = "Cancelled";
               _db.SaveChanges();
                return RedirectToAction("Index");
            }

        
    }
}
