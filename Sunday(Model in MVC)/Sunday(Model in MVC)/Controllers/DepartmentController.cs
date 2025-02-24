using Microsoft.AspNetCore.Mvc;
using Sunday_Model_in_MVC_.Models;
using System.Linq;

namespace Sunday_Model_in_MVC_.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly MyDbContext _context;

        public DepartmentController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var departments = _context.Departments.ToList(); 
            return View(departments); 
        }

     

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public IActionResult Edit(int id)
        {
            var department = _context.Departments.Find(id);
            return View(department);
        }

        [HttpPost]
        public IActionResult Edit(int id, Department department)
        {
           

            if (ModelState.IsValid)
            {
                _context.Departments.Update(department);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }
        public IActionResult Delete(int id)
        {
            var Department = _context.Departments.Find(id);
            if (Department == null) return NotFound();

            _context.Departments.Remove(Department);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var Department = _context.Departments.Find(id);
            return View(Department);
        }


    }
}
