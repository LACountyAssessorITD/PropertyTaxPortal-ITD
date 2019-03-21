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
using NLog;
using Microsoft.EntityFrameworkCore;

namespace PropertyTaxPortal.Controllers
{
    public class HomeController : Controller
    {     
        private readonly Email _email;
        private readonly IHostingEnvironment _host;
        private static Logger fileLogger = LogManager.GetLogger("fileLogger");
        private static Logger databaseLogger = LogManager.GetLogger("databaseLogger");
        private readonly PTPContext _context;

        public HomeController(IOptions<Email> email, IHostingEnvironment host, PTPContext context)
        {
            _email = email.Value;
            _host = host;
            _context = context;
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

        /// <summary>
        /// Public Inquiry form
        /// </summary>
        /// <returns></returns>
        public IActionResult PublicInquiry()
        {
            var model = new PublicInquiryViewModel();
            model.Subjects = GetAllSubjects();
            model.States = GetAllStates();
            model.propertyAddrState = "CA";

            return View(model);
        }

        private const string EmailAssessmentAppeals = "@bos.lacounty.gov";
        private const string EmailAssessor = "@assessor.lacounty.gov";
        private const string EmailAuditorController = "@auditor.lacounty.gov";
        private const string EmailTreasurer = "@ttc.lacounty.gov";

        /// <summary>
        /// Public Inquiry form with model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PublicInquiry(PublicInquiryViewModel model)
        {
            model.Subjects = GetAllSubjects();
            model.States = GetAllStates();

            if (ModelState.IsValid)
            {
                //Find the responsible department and address
                string sEmailDept = "";
                List<EmailTrackingCount> lEmailTracking = new List<EmailTrackingCount>();
                string sEmailTrackingCount = "";

                lEmailTracking = _context.emailTrackingCount.FromSql("PTP_emailTrackingIncrement").ToList();
                sEmailTrackingCount = lEmailTracking.First().emailTrackingCount.ToString();
                model.emailTrackingCount = sEmailTrackingCount;
                sEmailDept = model.subjectValue.Substring(model.subjectValue.IndexOf("@"), model.subjectValue.Length - model.subjectValue.IndexOf("@"));
                switch (sEmailDept)
                {
                    case EmailAssessmentAppeals:
                        model.responsibleDepartment = "Assessment Appeals Board";
                        model.addressPhoneWebsite = "Los Angeles County Assessment Appeals Board<br>" +
                            "500 West Temple Street, Room B50<br>" +
                            "Los Angeles, CA 90012<br><br>" +
                            "Phone:&nbsp;&nbsp;&nbsp;1(888)807-2111 (Toll Free) and press number 4<br>" +
                            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(213)974-1471<br><br>" +
                            "Website:&nbsp;&nbsp;&nbsp;<a href='http://bos.lacounty.gov/Services/AssessmentAppeals.aspx' target='newwin'>http://bos.lacounty.gov/Services/AssessmentAppeals.aspx</a><br>";
                        break;

                    case EmailTreasurer:
                        model.responsibleDepartment = "Treasurer & Tax Collector's department";
                        model.addressPhoneWebsite = "Los Angeles County Treasurer and Tax Collector<br>" +
                            "225 N. Hill Street<br>" +
                            "Los Angeles, CA 90012-2798<br><br>" +
                            "Phone:&nbsp;&nbsp;&nbsp;1(888)807-2111 (Toll Free)<br>" +
                            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(213)974-3211<br><br>" +
                            "Website:&nbsp;&nbsp;&nbsp;<a href='http://ttc.lacounty.gov' target='newwin'>http://ttc.lacounty.gov</a><br>";
                        break;

                    case EmailAuditorController:
                        model.responsibleDepartment = "Auditor-Controller's department";
                        model.addressPhoneWebsite = "Los Angeles County Auditor-Controller Public Service Section<br>" +
                            "500 West Temple Steet, Room 153<br>" +
                            "Los Angeles, CA 90012-2713<br><br>" +
                            "Phone:&nbsp;&nbsp;&nbsp;1(888)807-2111 (Toll Free)<br>" +
                            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(213)974-3211<br><br>" +
                            "Website:&nbsp;&nbsp;&nbsp;<a href='http://auditor.lacounty.gov' target='newwin'>http://auditor.lacounty.gov</a><br>";
                        break;

                    case EmailAssessor:
                        model.responsibleDepartment = "Assessor's department";
                        model.addressPhoneWebsite = "Los Angeles County Assessor Public Service Section<br>" +
                            "500 West Temple Steet, Room 225<br>" +
                            "Los Angeles, CA 90012-2713<br><br>" +
                            "Phone:&nbsp;&nbsp;&nbsp;1(888) 807-2111 (Toll Free)<br>" +
                            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(213) 974-3211<br><br>" +
                            "Website:&nbsp;&nbsp;&nbsp;<a href='http://assessor.lacounty.gov' target='newwin'>http://assessor.lacounty.gov</a><br>";
                        break;

                    default:
                        model.responsibleDepartment = "";
                        model.addressPhoneWebsite = "";
                        break;
                }

                string mFrom = (model.emailAddr.Trim() == "") ? _email.from : model.emailAddr.Trim();

                MailAddress from = new MailAddress(mFrom);
                MailAddress to;
                if (_host.IsProduction())
                    to = new MailAddress(model.subjectValue);
                else
                    to = new MailAddress(_email.mailTo);

                MailMessage mail = new MailMessage(from, to);
                string strSubjectText = model.subjectText;
                string sSubject = strSubjectText + " - " + _email.subject + " - Reference # " + sEmailTrackingCount;

                mail.Subject = sSubject;
                mail.IsBodyHtml = _email.isBodyHtml;
                mail.Priority = MailPriority.Normal;
                MailAddress bcc = new MailAddress(_email.bcc);
                mail.Bcc.Add(bcc);

                mail.Body = BuildMailBody("/Templates/EmailTemplate/PublicInquiry.html", model);
                try
                {
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = _email.host;
                    smtp.Port = _email.port;
                    smtp.EnableSsl = _email.enableSsl;
                    smtp.Send(mail); //email to responsible department and bcc emailsbk@assessor.lacounty.gov
                    if (model.emailAddr.Trim() != "")
                    {   
                        from = new MailAddress(_email.from);
                        to = new MailAddress(model.emailAddr.Trim());
                        mail = new MailMessage(from, to);
                        mail.Subject = sSubject;
                        mail.IsBodyHtml = _email.isBodyHtml;
                        mail.Priority = MailPriority.Normal;
                        mail.Body = BuildMailBody("/Templates/EmailTemplate/PublicInquiryUser.html", model);

                        smtp.Send(mail); //email to user
                    }
                }
                catch (Exception ex)
                {
                    fileLogger.Error("Error saved in file");
                    databaseLogger.Error(ex, "Error occured in sending the email");
                    return View("~/Views/Shared/_ErrorMessage.cshtml");
                }

                return View("PublicInquiryThankYou", model);
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// Build the body of mail
        /// </summary>
        /// <param name="path"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public string BuildMailBody (string path, PublicInquiryViewModel model)
        {
            var wwwRoot = _host.WebRootPath;
            string sOriginalBody = "";

            using (StreamReader reader = new StreamReader(wwwRoot + path))
            {
                sOriginalBody = reader.ReadToEnd();

                sOriginalBody = sOriginalBody.Replace("{Subject}", model.subjectText);
                sOriginalBody = sOriginalBody.Replace("{ResponsibleDepartment}", string.IsNullOrEmpty(model.responsibleDepartment) ? "" : model.responsibleDepartment);
                sOriginalBody = sOriginalBody.Replace("{LastName}", model.lastName);
                sOriginalBody = sOriginalBody.Replace("{FirstName}", model.firstName);
                sOriginalBody = sOriginalBody.Replace("{BusinessName}", string.IsNullOrEmpty(model.businessName) ? "" : model.businessName);
                sOriginalBody = sOriginalBody.Replace("{MailingAddr}", string.IsNullOrEmpty(model.mailingAddr) ? "" : model.mailingAddr);
                sOriginalBody = sOriginalBody.Replace("{MailAddrCity}", string.IsNullOrEmpty(model.mailAddrCity) ? "" : model.mailAddrCity);
                sOriginalBody = sOriginalBody.Replace("{MailAddrState}", string.IsNullOrEmpty(model.mailAddrState) ? "" : model.mailAddrState);
                sOriginalBody = sOriginalBody.Replace("{MailAddrZip}", string.IsNullOrEmpty(model.mailAddrZip) ? "" : model.mailAddrZip);
                sOriginalBody = sOriginalBody.Replace("{EmailAddr}", model.emailAddr);
                sOriginalBody = sOriginalBody.Replace("{DayTimeTelNumber}", model.dayTimeTelNumber);
                sOriginalBody = sOriginalBody.Replace("{FaxNumber}", string.IsNullOrEmpty(model.faxNumber) ? "" : model.faxNumber);
                sOriginalBody = sOriginalBody.Replace("{PropertyAddr}", string.IsNullOrEmpty(model.propertyAddr) ? "" : model.propertyAddr);
                sOriginalBody = sOriginalBody.Replace("{PropertyAddrCity}", string.IsNullOrEmpty(model.propertyAddrCity) ? "" : model.propertyAddrCity);
                sOriginalBody = sOriginalBody.Replace("{PropertyAddrState}", string.IsNullOrEmpty(model.propertyAddrState) ? "" : model.propertyAddrState);
                sOriginalBody = sOriginalBody.Replace("{PropertyAddrZip}", string.IsNullOrEmpty(model.propertyAddrZip) ? "" : model.propertyAddrZip);
                sOriginalBody = sOriginalBody.Replace("{AIN}", string.IsNullOrEmpty(model.AIN) ? "" : model.AIN);
                sOriginalBody = sOriginalBody.Replace("{CompanyNumber}", string.IsNullOrEmpty(model.companyNumber) ? "" : model.companyNumber);
                sOriginalBody = sOriginalBody.Replace("{RoutingIndex}", string.IsNullOrEmpty(model.routingIndex) ? "" : model.routingIndex);
                string sComment = model.comment.Replace("\r\n", "<br>");
                sOriginalBody = sOriginalBody.Replace("{Comments}", string.IsNullOrEmpty(model.comment) ? "" : sComment);
                sOriginalBody = sOriginalBody.Replace("{EmailTrackingCount}", string.IsNullOrEmpty(model.emailTrackingCount) ? "" : model.emailTrackingCount);
            }

            return sOriginalBody;
        }

        /// <summary>
        /// Public Inquiry Thank You page
        /// </summary>
        /// <returns></returns>
        public IActionResult PublicInquiryThankYou()
        {
            return View();
        }

        /// <summary>
        /// Pull the subject data for the public inquiry form subject dropdown
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetAllSubjects()
        {
            List<Subjects> lSubjects = new List<Subjects>();
            lSubjects = _context.subjects.FromSql("PTP_getAllSubjects").ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            foreach (var oneSubject in lSubjects)
            {
                li.Add(new SelectListItem { Text = oneSubject.Description, Value = oneSubject.Email });
            }
            IEnumerable<SelectListItem> item = li.AsEnumerable();
            return item;
        }

        /// <summary>
        /// Pull the States data for the public inquiry form States dropdown
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetAllStates()
        {
            List<States> lStates = new List<States>();
            lStates = _context.states.FromSql("PTP_getAllStates").ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            foreach (var oneState in lStates)
            {
                if (oneState.State == "CA")
                    li.Add(new SelectListItem { Text = oneState.State, Value = oneState.State, Selected = true });
                else
                    li.Add(new SelectListItem { Text = oneState.State, Value = oneState.State });
            }
            IEnumerable<SelectListItem> item = li.AsEnumerable();
            return item;
        }
    }
}
