using EcommerceDemoWeb.Data;
using EcommerceDemoWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceDemoWeb.Controllers
{
    [Area("Admin")]
    public class SellerController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SellerController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Seller> objSellerList = _db.Seller.ToList();
            return View(objSellerList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Seller obj)
        {

            if (ModelState.IsValid)
            {
                _db.Seller.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Seller.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Seller obj)
        {

            if (ModelState.IsValid)
            {
                _db.Seller.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

       

        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Seller.Find(id);            
            if (obj == null)
            {
                return NotFound();
            }
            _db.Seller.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
