using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MasterPiece.Services;
using MasterPiece.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;



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
            
        }

        // --------- login ---------

        public ActionResult login()
        {
            return View();
        }




        // -------------- Login ---------------
        [HttpPost]
        public async Task<IActionResult> login(User model)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(h => h.Email == model.Email && h.Password == model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }

            HttpContext.Session.SetString("UserLoggedIn", "true");
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.Name ?? "Unknown");
            HttpContext.Session.SetString("UserEmail", user.Email ?? "No Email");
            HttpContext.Session.SetString("UserPhone", user.Phone ?? "No Phone");
            HttpContext.Session.SetString("UserAddress", user.Address ?? "No address provided");
            HttpContext.Session.SetString("UserImage", user.Image ?? "/uploads/default.jpg");

            return RedirectToAction("Index", "Home");
        }






        // --------- profile --------------------

        public IActionResult Profile()
        {
            var UserId = HttpContext.Session.GetInt32("UserId");

            

            if (UserId == null)
            {
                return RedirectToAction("Login"); // إذا لم يكن هناك جلسة، إعادة توجيه إلى صفحة تسجيل الدخول
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == UserId);
            if (user == null)
            {
                return RedirectToAction("Login"); // إذا لم يتم العثور على المستخدم في قاعدة البيانات
            }

      

            return View(user);
        }




        // --------- Logout ---------

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // مسح الجلسة
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
          
            string email = HttpContext.Session.GetString("ResetEmail");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("ForgotPassword"); 
            }

            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string newPassword)
        {
            string email = HttpContext.Session.GetString("ResetEmail");

            if (!string.IsNullOrEmpty(email))
            {
                
                var User = _context.Users.FirstOrDefault(h => h.Email == email);
           

                if (User != null)
                {
                    User.Password = newPassword; 
                }
                
                else
                {
                    ViewBag.Error = "User not found.";
                    return View();
                }

                _context.SaveChanges(); 
                HttpContext.Session.Remove("ResetEmail"); 

                ViewBag.Message = "Your password has been reset successfully!";
            }
            return RedirectToAction(nameof(login));
        }



        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }





        // ------- Register -------------

 

        [HttpPost]
        public async Task<IActionResult> Register(User model, IFormFile profilePic)
        {
           
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    Console.WriteLine("Validation Error: " + error); // طباعة الأخطاء في الـ Console
                }
                return View("login", model);
            }

            // التأكد من أن الإيميل غير مسجل مسبقًا
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Email is already registered.");
                return View("login", model);
            }

            if (string.IsNullOrEmpty(model.Phone))
            {
                model.Phone = "000-000-0000";
            }
                // معالجة رفع الصورة
                if (profilePic != null && profilePic.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
              

                var extension = Path.GetExtension(profilePic.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("profilePic", "The file type is not supported. Please upload a .jpg, .jpeg, or .png file.");
                    
                    return View("login", model);
                }


                var fileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePic.CopyToAsync(stream);
                }

                model.Image = "/uploads/" + fileName;
            }
            else
            {
                model.Image = "/uploads/default.jpg";
            }

            // تشفير كلمة المرور
            model.Password = HashPassword(model.Password);

            // حفظ المستخدم في قاعدة البيانات
            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            Console.WriteLine("User Registered Successfully!");
            return RedirectToAction("login");
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }








       
        //------------Edit profile ------------
        public IActionResult EditProfile(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }




        [HttpPost]
        public async Task<IActionResult> EditProfile(User model, IFormFile profilePic)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("login");

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return RedirectToAction("login");

            // تحديث البيانات الشخصية
            user.Name = model.Name;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.Address = model.Address;

            // إذا تم رفع صورة جديدة
            if (profilePic != null && profilePic.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
               

                var extension = Path.GetExtension(profilePic.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("profilePic", "The file type is not supported. Please upload a .jpg, .jpeg, or .png file.");
                    return View("Profile", user);
                }


                // حذف الصورة القديمة إذا لم تكن الصورة الافتراضية
                if (!string.IsNullOrEmpty(user.Image) && user.Image != "/uploads/default.jpg")
                {
                    var oldFilePath = Path.Combine("wwwroot", user.Image.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                        System.IO.File.Delete(oldFilePath);
                }

                // حفظ الصورة الجديدة
                var fileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine("wwwroot/uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePic.CopyToAsync(stream);
                }

                user.Image = "/uploads/" + fileName;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // تحديث الجلسة بعد التعديل
            HttpContext.Session.SetString("UserName", user.Name ?? "Unknown");
            HttpContext.Session.SetString("UserEmail", user.Email ?? "No Email");
            HttpContext.Session.SetString("UserPhone", user.Phone ?? "No Phone");
            HttpContext.Session.SetString("UserAddress", user.Address ?? "No address provided");
            HttpContext.Session.SetString("UserImage", user.Image ?? "/uploads/default.jpg");

            return RedirectToAction("Profile");
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
