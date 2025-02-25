using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using example25_2.Models;
using Microsoft.EntityFrameworkCore;

namespace example25_2.Controllers;

public class HomeController : Controller
{
    private readonly MyDbContext _context;

    public HomeController(MyDbContext context)
    {
        _context = context;
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult login(User user)
    {
        if (ModelState.IsValid)
        {
            var loginUser = _context.Users.FirstOrDefault(recourd => recourd.Email == user.Email && recourd.Password == user.Password);
            if (loginUser != null)
            {
                if (loginUser.Role == "Admin")
                {
                    return RedirectToAction("Admin");
                }

                else if (loginUser.Role == "SuperAdmin")
                {
                    return RedirectToAction("SuperAdmin");
                }

                else
                    return RedirectToAction("Index");

            }
            else
                return NotFound();
        }
        ViewBag.Error = "invalid input";
        return View();
    }
    public IActionResult login()
    {
        return View();

    }

   
    public async Task<IActionResult> Admin()
    {
        var users = await _context.Users.ToListAsync();
        return View(users);

    }
    public IActionResult SuperAdmin()
    {
        return View();

    }
    public IActionResult Index()
    {
        return View();
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
