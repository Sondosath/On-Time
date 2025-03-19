using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MasterPiece.Services;
using MasterPiece.Models;


namespace MasterPiece.Controllers
{
    public class UserController : Controller
    {
        private readonly EmailService _emailService;
        private readonly MyDbContext _context;

        public UserController( MyDbContext context, EmailService emailService)
        {
            _emailService = emailService;
            _context = context;
            _emailService = emailService;
        }

        // --------- login ---------

        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> login(User model)
        {
            //if (!ModelState.IsValid)
            //    return View(model);

            var User = await _context.Users
                .FirstOrDefaultAsync(h => h.Email == model.Email && h.Password == model.Password);

            if (User == null)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }


            HttpContext.Session.SetString("UserLoggedIn", "true");
            HttpContext.Session.SetInt32("HrID", User.Id);
            HttpContext.Session.SetString("HrName", User.Name ?? ""); 
            HttpContext.Session.SetString("HrEmail", User.Email ?? "");
           

            return RedirectToAction("Index", "Home");
        }

        // --------- Logout ---------
        public IActionResult Logout()
        {

            HttpContext.Session.Remove("UserLoggedIn");
            return RedirectToAction("Index", "Home");

        }


        // --------- ForgotPassword ---------
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                // تخزين البريد الإلكتروني في Session
                HttpContext.Session.SetString("ResetEmail", email);

                // تحقق مما إذا كان البريد موجودًا في قاعدة البيانات
                var User = _context.Users.FirstOrDefault(h => h.Email == email);
             

                if (User != null)
                {
                    // إنشاء رابط إعادة التعيين
                    string resetLink = Url.Action("ResetPassword", "User", null, Request.Scheme);
                    string message = $"Click <a href='{resetLink}'>here</a> to reset your password.";

                    await _emailService.SendEmailAsync(email, "Reset Your Password", message);
                    ViewBag.Message = "Password reset email has been sent!";
                }
                else
                {
                    ViewBag.Error = "Email not found in our system.";
                }
            }

            return View();
        }




        //--------Reset password ---------

        [HttpGet]
        public IActionResult ResetPassword()
        {
            // استرجاع البريد الإلكتروني من السيشن
            string email = HttpContext.Session.GetString("ResetEmail");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("ForgotPassword"); // إذا لم يكن هناك بريد مخزن، أعده إلى صفحة نسيان كلمة المرور
            }

            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string newPassword)
        {
            string email = HttpContext.Session.GetString("ResetEmail");

            if (!string.IsNullOrEmpty(email))
            {
                // البحث عن المستخدم في قاعدة البيانات
                var User = _context.Users.FirstOrDefault(h => h.Email == email);
           

                if (User != null)
                {
                    User.Password = newPassword; // تحديث كلمة المرور
                }
                
                else
                {
                    ViewBag.Error = "User not found.";
                    return View();
                }

                _context.SaveChanges(); // حفظ التغييرات في قاعدة البيانات
                HttpContext.Session.Remove("ResetEmail"); // مسح البريد من السيشن بعد الاستخدام

                ViewBag.Message = "Your password has been reset successfully!";
            }
            return RedirectToAction(nameof(login));
        }



        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (!ModelState.IsValid)
                return View("login", model);

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Email is already registered.");
                return View("login", model);
            }

            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("login");
        }
        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
