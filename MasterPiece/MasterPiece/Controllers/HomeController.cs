using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MasterPiece.Models;
using Microsoft.EntityFrameworkCore;
using MasterPiece.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace MasterPiece.Controllers;

public class HomeController : Controller
{
    private readonly MyDbContext _context;

   

    private readonly ILogger<HomeController> _logger;

    public HomeController(MyDbContext context,ILogger<HomeController> logger)
    {
        _logger = logger;
        _context = context;

    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult ContactUs()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ContactUs(ContactU contactMessage)
    {
        if (contactMessage == null || string.IsNullOrWhiteSpace(contactMessage.Name) ||
            string.IsNullOrWhiteSpace(contactMessage.Email) || string.IsNullOrWhiteSpace(contactMessage.Message))
        {
            return Json(new { success = false, message = "All fields are required." });
        }

        contactMessage.CreatedAt = DateTime.Now;

        _context.ContactUs.Add(contactMessage);
        await _context.SaveChangesAsync();

        return Json(new { success = true, message = "Message sent successfully!" });
    }





    public IActionResult about()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
