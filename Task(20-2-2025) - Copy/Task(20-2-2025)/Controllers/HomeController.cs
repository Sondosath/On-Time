using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Task_20_2_2025_.Models;

namespace Task_20_2_2025_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Name = HttpContext.Session.GetString("Email");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterHandel(IFormCollection form)
        {
            string name = form["Name"];
            string email = form["Email"];
            string password = form["Password"];
            string confirmPassword = form["ConfirmPassword"];


            
            if (password != confirmPassword)
            {
                ViewData["ErrorMessage"] = "THE PASSOWEDS DONT MATCH!";
                return View();
            }


            HttpContext.Session.SetString("Name", name);
            HttpContext.Session.SetString("Email", email);
            HttpContext.Session.SetString("Password", password);


            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {// Read
            var data = Request.Cookies["userInfo"];
            if (data != null)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }
        public IActionResult HandleLogin(string user, string pass, string rem)
        {// write
            
                if (rem != null)
                {
                    CookieOptions obj = new CookieOptions();
                    obj.Expires = DateTime.Now.AddDays(2);   //store
                    Response.Cookies.Append("userInfo", user, obj);
                }
                return RedirectToAction("Index", "Home");
            
            return View();
        }


        public IActionResult Profile()
        {
            
            if (ViewBag.Email == "")
            {
                ViewData["NoUserLogin"] = "please Login First";
                return RedirectToAction("Index", "Login");
            }
            else {
                ViewBag.UserName = HttpContext.Session.GetString("Name");
                ViewBag.Email = HttpContext.Session.GetString("Email");
                ViewData["Password"] = HttpContext.Session.GetString("Password");
                return View();
            }

            
        }
        public IActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditProfileHandel(IFormCollection form)
        {
            string name = form["Name"];
            string email = form["Email"];
            string Address = form["Address"];
            string location = form["location"];


            HttpContext.Session.SetString("Name", name);
            HttpContext.Session.SetString("Email", email);
            HttpContext.Session.SetString("location", location);
            HttpContext.Session.SetString("Address", Address);

            return RedirectToAction("Index", "EditProfile");




        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
