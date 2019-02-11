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
            LoadDepartments();
            return View();
        }

        [HttpPost]
        public IActionResult PublicInquiry(PublicInquiryViewModel model)
        {
            using (MailMessage mm = new MailMessage(_email.from, "wlam@assessor.lacounty.gov"))
            {
                mm.Subject = model.selectedSubject + " - " + _email.subject + " - Reference # 85566";
                mm.Body = mm.Body + "Last Name: " + model.lastName + "<br>" + "First Name: " + model.firstName + "<br>";
                mm.IsBodyHtml = _email.isBodyHtml;

                var wwwRoot = _host.WebRootPath;
                using (StreamReader reader = new StreamReader(wwwRoot + "/Templates/EmailTemplate/PublicInquiry.html"))
                {
                    mm.Body = reader.ReadToEnd();
                    mm.Body = mm.Body.Replace("{SentTo}", model.selectedEmail);
                    mm.Body = mm.Body.Replace("{LastName}", model.lastName);
                    mm.Body = mm.Body.Replace("{FirstName}", model.firstName);
                    mm.Body = mm.Body.Replace("{comment}", model.comment);
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

        public ActionResult LoadDepartments()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select", Value = "0" });
            li.Add(new SelectListItem { Text = "Assessment Appeals Board", Value = "1" });
            li.Add(new SelectListItem { Text = "Assessor", Value = "2" });
            li.Add(new SelectListItem { Text = "Auditor-Controller", Value = "3" });
            li.Add(new SelectListItem { Text = "Treasurer & Tax Collector", Value = "4" });

            ViewData["departments"] = li;
            return View();
        }

        [HttpPost]
        public JsonResult GetSubjects(string id)
        {
            List<SelectListItem> subjects = new List<SelectListItem>();

            switch (id)
            {
                case "1":
                    //subjects.Add(new SelectListItem { Text = "Select", Value = "0" });
                    subjects.Add(new SelectListItem { Text = "Assessment Appeals", Value = "01-aaboffice@bos.lacounty.gov" });
                    break;
                case "2":
                    subjects.Add(new SelectListItem { Text = "Change Mailing Address", Value = "02-helpdesk@assessor.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Exemptions", Value = "04-helpdesk@assessor.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Incorrect Property Information", Value = "05-helpdesk@assessor.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Ownership", Value = "09-helpdesk@assessor.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Reassessment Exclusions", Value = "15-helpdesk@assessor.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Values / Decline in Value", Value = "23-helpdesk@assessor.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Website Related Inquiries", Value = "24-webmaster@assessor.lacounty.gov" });
                    break;
                case "3":
                    subjects.Add(new SelectListItem { Text = "Direct Assessments", Value = "03-propertytax@auditor.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Property Tax Claim for Refunds", Value = "14-propertytax@auditor.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Refunds(value reductions)", Value = "16-propertytax@auditor.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Tax Adjustment", Value = "17-propertytax@auditor.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Tax Rates", Value = "20-propertytax@auditor.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Transfer Taxes to the Prior Owner", Value = "21-propertytax@auditor.lacounty.gov" });
                    break;
                case "4":
                    subjects.Add(new SelectListItem { Text = "Liens", Value = "06-unsecured@ttc.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Lost Tax Bill / Tax Bill Request", Value = "07-info@ttc.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Overpayment / Refund", Value = "08-info@ttc.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Payment (all others)", Value = "10-info@ttc.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Payment (online credit card)", Value = "11-ccard@ttc.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Payments (online eCheck)", Value = "12-echeck@ttc.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "PIN Request", Value = "13-echeck@ttc.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Tax Auction", Value = "18-auction@ttc.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Tax Penalty Issue", Value = "19-info@ttc.lacounty.gov" });
                    subjects.Add(new SelectListItem { Text = "Unsecured Bills", Value = "22-unsecured@ttc.lacounty.gov" });
                    break;
            }

            return this.Json(new SelectList(subjects, "Value", "Text"));
        }
    }
}
