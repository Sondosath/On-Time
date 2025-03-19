using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aspTask7.Models;
using Microsoft.EntityFrameworkCore;

namespace aspTask7.Controllers;

public class HomeController : Controller
{
    private readonly MyDbContext _context;

    public HomeController(MyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {

        return RedirectToAction("Index", "Products");

    }
    public IActionResult login()
    {
        return View();
    }

    [HttpPost]
  
    public IActionResult login(User user)
    {
        if (ModelState.IsValid)
        {
            var loginUser = _context.Users.FirstOrDefault(recourd => recourd.Email == user.Email && recourd.Password == user.Password);
            if (loginUser != null)
            {
                HttpContext.Session.SetString("UserEmail", loginUser.Email);
                if (loginUser.Role == "Admin")
                {
                    return RedirectToAction("Admin");
                }

                else
                    return RedirectToAction("Index");

            }
            else
                return NotFound();
        }
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
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(user);
    }





   

    public async Task<IActionResult> Profile(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }



    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("login");
    }




    public IActionResult EditProfile(int? id)
    {
        var user = _context.Users.Find(id);
        return View(user);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}