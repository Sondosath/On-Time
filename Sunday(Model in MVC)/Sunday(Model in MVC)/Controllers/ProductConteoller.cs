using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sunday_Model_in_MVC_.Models;

namespace Sunday_Model_in_MVC_.Controllers
{
    public class ProductConteoller : Controller
    {
        private readonly MyDbContextProduct _context;

        public ProductConteoller(MyDbContextProduct context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var Product = _context.Product.ToList();
            return View(Product);
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product Product)
        {
            if (ModelState.IsValid)
            {
                _context.Product.Add(Product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Product);
        }

        public IActionResult Edit(int id)
        {
            var Product = _context.Product.Find(id);
            return View(Product);
        }

        [HttpPost]
        public IActionResult Edit(int id, Product Product)
        {


            if (ModelState.IsValid)
            {
                _context.Product.Update(Product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Product);
        }
        public IActionResult Delete(int id)
        {
            var Department = _context.Product.Find(id);
            if (Department == null) return NotFound();

            _context.Product.Remove(Department);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var Product = _context.Product.Find(id);
            return View(Product);
        }
    }
}
