using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasterPiece.Models;
using MasterPiece.Helpers;

namespace MasterPiece.Controllers
{
    public class ShopController : Controller
    {
        // GET: ShopController
        private readonly MyDbContext _context;

        public ShopController(MyDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.Include(c => c.Products).ToListAsync();
            return View(categories);
        }

        // GET: ShopController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShopController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShopController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShopController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShopController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        //-------------Category Products----------------

        public IActionResult CategoryProducts(int id)
        {
            
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            
            var products = _context.Products
                .Where(p => p.CategoryId == id)
                .ToList();

            ViewBag.CategoryName = category.Name; 

            return View(products); 
        }




        //-------------Product Details----------------
        public IActionResult ProductDetails(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }



        //-------------AddToCart-----------
        [HttpPost]
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }

            var userId = HttpContext.Session.GetInt32("UserId"); 
            var cart = _context.Carts.FirstOrDefault(c => c.UserId == userId && c.CreatedAt == null);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId =(int)userId,
                    CreatedAt = DateTime.Now,
                    CartDetails = new List<CartDetail>()
                };

                _context.Carts.Add(cart);
                _context.SaveChanges();
            }

            var cartDetail = cart.CartDetails.FirstOrDefault(cd => cd.ProductId == productId);

            if (cartDetail != null)
            {
                cartDetail.Quantity += quantity;  // إذا كان المنتج موجوداً مسبقاً في السلة، نضيف الكمية الجديدة
            }
            else
            {
                cartDetail = new CartDetail
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price
                };
                cart.CartDetails.Add(cartDetail);
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Shop");  // إعادة توجيه المستخدم بعد إضافة المنتج
        }




        // GET: ShopController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShopController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
