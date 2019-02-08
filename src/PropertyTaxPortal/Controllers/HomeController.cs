using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PropertyTaxPortal.Models;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace PropertyTaxPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly Email _email;
        private readonly IHostingEnvironment _host;

        public HomeController(IOptions<Email> email, IHostingEnvironment host)
        {
            _email = email.Value;
            _host = host;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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

        public IActionResult PublicInquiry()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PublicInquiry(PublicInquiryViewModel model)
        {
            using (MailMessage mm = new MailMessage(_email.from, "wlam@assessor.lacounty.gov"))
            {
                mm.Subject = "Assessment Appeals - " + _email.subject + " - Reference # 85566";
                mm.Body = mm.Body + "Last Name: " + model.lastName + "<br>" + "First Name: " + model.firstName + "<br>";
                mm.IsBodyHtml = _email.isBodyHtml;

                var wwwRoot = _host.WebRootPath;
                using (StreamReader reader = new StreamReader(wwwRoot + "/Templates/EmailTemplate/PublicInquiry.html"))
                {
                    mm.Body = reader.ReadToEnd();
                    //Replace UserName and Other variables available in body Stream
                    mm.Body = mm.Body.Replace("{LastName}", model.lastName);
                    mm.Body = mm.Body.Replace("{FirstName}", model.firstName);
                }

                try
                {
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = _email.host;
                    smtp.Port = _email.port;
                    smtp.EnableSsl = _email.enableSsl;
                    smtp.Send(mm);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return View("PublicInquiryThankYou");
            }
        }

        public IActionResult PublicInquiryThankYou()
        {
            return View();
        }

    }
}
