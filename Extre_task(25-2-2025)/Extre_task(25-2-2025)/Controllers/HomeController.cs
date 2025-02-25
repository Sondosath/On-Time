using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Extre_task_25_2_2025_.Models;
using Microsoft.EntityFrameworkCore;

namespace Extre_task_25_2_2025_.Controllers;

public class HomeController : Controller


{


    private readonly MyDbContext _context;

    public HomeController(MyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int id)
    {
        var user = _context.Users.Find(id);
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

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }
}
