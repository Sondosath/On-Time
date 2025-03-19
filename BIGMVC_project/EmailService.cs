using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BIGMVC_project.Services
{
    public class EmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com"; // SMTP Server
        private readonly int _smtpPort = 587; // SMTP Port
        private readonly string _smtpUsername = "sondos.3th@gmail.com"; 
        private readonly string _smtpPassword = "xlyl mhpy nzxt aved";


        public async Task SendPasswordResetEmailAsync(string userEmail, string resetLink)
        {
            // إعداد العميل لإرسال البريد الإلكتروني
            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                // توفير بيانات المصادقة
                client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);

                // تفعيل الاتصال المشفر باستخدام SSL أو TLS
                client.EnableSsl = true;  // تأكد من تفعيل SSL أو TLS

                // إعداد الرسالة البريدية
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpUsername), 
                    Subject = "Reset Your Password",       
                    Body = $"Click the link to reset your password: <a href='{resetLink}'>Reset Password</a>", 
                    IsBodyHtml = true   
                };

                // إرسال البريد إلى المستخدم
                mailMessage.To.Add(userEmail); // البريد الذي أدخله المستخدم في الفورم

                // إرسال الرسالة بشكل غير متزامن
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
