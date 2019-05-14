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
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

namespace PropertyTaxPortal.Controllers
{
    public class HomeController : Controller
    {     
        private readonly Email _email;
        private readonly IHostingEnvironment _host;
        private static Logger fileLogger = LogManager.GetLogger("fileLogger");
        private static Logger databaseLogger = LogManager.GetLogger("databaseLogger");
        private readonly PTPContext _context;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IOptions<Email> email, IHostingEnvironment host, PTPContext context, IStringLocalizer<HomeController> localizer)
        {
            _email = email.Value;
            _host = host;
            _context = context;
            _localizer = localizer;
        }


        public IActionResult Index()
        {
            var context = _context.News.Where(b => (b.Active.Equals("Featured") && DateTime.Compare(b.EndOn, DateTime.Now) > 0)).OrderBy(b => b.SOrder).ToList();
            return View(context);
        }

        //Annual Secured Property Tax Bill
        public IActionResult AnnualSecuredProperty()
        {
            return View();
        }

        //Annual Secured Property Tax Information Statement
        public IActionResult AnnualSecuredPropertyTaxInformationStatement()
        {
            return View();
        }

        //Notice of Delinquency
        public IActionResult NoticeofDelinquency()
        {
            return View();
        }

        //Substitute Secured Property Tax Bill
        public IActionResult SubstitueSecuredProperty()
        {
            return View();
        }

        //Supplemental Secured Property Tax Bill 
        public IActionResult SupplementalSecuredProperty()
        {
            return View();
        }

        //Unsecured Property Tax Bill
        public IActionResult UnSecuredPropertyTax()
        {
            return View();
        }

        //Unsecured Prior Year Bill 
        public IActionResult UnSecuredPriorYear()
        {
            return View();
        }

        //Payment Activity Notice
        public IActionResult PaymentActivityNotice()
        {
            return View();
        }

        //Statement of Prior Year Taxes
        public IActionResult StatementOfPriorYear()
        {
            return View();
        }

        //Adjusted Annual Property Tax Bill
        public IActionResult AdjAnnual()
        {
            return View();
        }

        //Adjusted Supplemental Property Tax Bill
        public IActionResult AdjSupplemental()
        {
            return View();
        }

        public IActionResult Accessibility(string id)
        {
            Dictionary<string, string> titles = new Dictionary<string, string>();
            titles.Add("disclaimer","DISCLAIMER");
            titles.Add("privacy-policy","PRIVACY POLICY");
            titles.Add("accessibility", "ACCESSIBILITY");
            titles.Add("language", "LANGUAGE");
            titles.Add("browser-compatibility", "BROWSER COMPATIBILITY");


            if (!titles.ContainsKey(id.ToLower()))
            {
                return View();
            }
            else
            {
                
                ViewBag.Title = titles[id.ToLower()];
                
                return View();
            }
        }

        public IActionResult ContestingValue()
        {
            ViewBag.Title = "CONTESTING YOUR PROPERTY VALUE";
            return View();
        }

        public IActionResult HowMuchDoIOwe()
        {
            ViewBag.Title = "PAYMENT INFORMATION";
            return View();
        }

        public IActionResult PrivacyPolicy(int? id)
        {
            ViewBag.Title = "PRIVACY POLICY";
            return View();
        }

        public IActionResult Language()
        {
            ViewBag.Title = "LANGUAGE TRANSLATION";
            return View();
        }

        public IActionResult Disclaimer()
        {
            ViewBag.Title = "DISCLAIMER";
            return View();
        }

        public IActionResult BrowserCompatibility()
        {
            ViewBag.Title = "BROWSER COMPATIBILITY";
            return View();
        }

        public IActionResult GovernmentSites()
        {
            ViewBag.Title = "OTHER GOVERNMENT SITES";
            return View();
        }
   
        public async Task<IActionResult> NewsLanding(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }
        public IActionResult News()
        {
            var context = _context.News.Where(b => ((b.Active.Equals("Featured") || b.Active.Equals("Current")) && DateTime.Compare(b.EndOn, DateTime.Now) > 0)).OrderBy(b => b.SOrder).ToList();
            return View(context);
        }


        public IActionResult Overview(int?id)
        {
            if (id == null) {
                return View();
            } else {
                ViewBag.id = id;
                
                return View();
            }
            
        }

        
        public IActionResult GeneralFAQ(int?id)
        {           
            dynamic FaqFinalModel = new System.Dynamic.ExpandoObject();
            var modelTaxBillFAQs = _context.faq.FromSql("PTP_getAllFAQs").Where(f => f.CategoryID == 1).OrderBy(f => f.sOrder);
            var modelRefundFAQs = _context.faq.FromSql("PTP_getAllFAQs").Where(f => f.CategoryID == 2).OrderBy(f => f.sOrder);
            var modelPropertyFAQs = _context.faq.FromSql("PTP_getAllFAQs").Where(f => f.CategoryID == 3).OrderBy(f => f.sOrder);
            var modelOwnershipFAQs = _context.faq.FromSql("PTP_getAllFAQs").Where(f => f.CategoryID == 4).OrderBy(f => f.sOrder);
            var modelTaxAgentFAQs = _context.faq.FromSql("PTP_getAllFAQs").Where(f => f.CategoryID == 5).OrderBy(f => f.sOrder);
            FaqFinalModel.TaxBill = modelTaxBillFAQs;
            FaqFinalModel.Refund = modelRefundFAQs;
            FaqFinalModel.Property = modelPropertyFAQs;
            FaqFinalModel.Ownership = modelOwnershipFAQs;
            FaqFinalModel.TaxAgent = modelTaxAgentFAQs;
            FaqFinalModel.tabid = id;            
            return View(FaqFinalModel);
        }

        /// <summary>
        /// Contact Us
        /// </summary>
        /// <returns></returns>
        public IActionResult ContactUs(int?id)
        {
            //dynamic ContactUsModel = new System.Dynamic.ExpandoObject();
            //ContactUsModel.tabid = id;
            //return View(ContactUsModel);

            if (id == null)
            {
                return View();
            }
            else
            {
                ViewBag.id = id;
                return View();
            }
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Public Inquiry Form
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
        /// Public Inquiry Form with model
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
                string sSubjectEnglish = "";
                string sEmail = "";
                string sEmailDept = "";
                List<EmailTrackingCount> lEmailTracking = new List<EmailTrackingCount>();
                string sEmailTrackingCount = "";
                string sThankYouFront = "A staff member from the ";
                string sThankYouEnd = " will respond to your request shortly.";
                string sThankYouWhole = "";

                lEmailTracking = _context.emailTrackingCount.FromSql("PTP_emailTrackingIncrement").ToList();
                sEmailTrackingCount = lEmailTracking.First().emailTrackingCount.ToString();
                model.emailTrackingCount = sEmailTrackingCount;
                sSubjectEnglish = model.subjectValue.Substring(0, model.subjectValue.IndexOf("|")); // Assessment Appeals
                model.subjectEnglish = sSubjectEnglish; // Assessment Appeals
                sEmail = model.subjectValue.Substring(model.subjectValue.IndexOf("|")+1, model.subjectValue.Length - model.subjectValue.IndexOf("|")-1); // aaboffice@bos.lacounty.gov
                sEmailDept = model.subjectValue.Substring(model.subjectValue.IndexOf("@"), model.subjectValue.Length - model.subjectValue.IndexOf("@")); // "@bos.lacounty.gov
                switch (sEmailDept)
                {
                    case EmailAssessmentAppeals:
                        sThankYouWhole = sThankYouFront + "Assessment Appeals Board" + sThankYouEnd;
                        model.responsibleDepartment = _localizer[sThankYouWhole];
                        model.Department = "Assessment Appeals Board";
                        model.addressPhoneWebsite = "Los Angeles County Assessment Appeals Board<br>" +
                            "500 West Temple Street, Room B4<br>" +
                            "Los Angeles, CA 90012<br><br>" +
                            _localizer["Phone"] + ":&nbsp;&nbsp;&nbsp;(888) 807-2111 (" + _localizer["Toll Free"] + ") and press number 5<br>" +
                            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(213) 974-1471<br><br>" +
                            _localizer["Website"] + ":&nbsp;&nbsp;&nbsp;<a class=\"dont-break-out\" href='http://bos.lacounty.gov/Services/AssessmentAppeals.aspx' target='newwin'>http://bos.lacounty.gov/Services/AssessmentAppeals.aspx</a><br>";
                        break;

                    case EmailTreasurer:
                        sThankYouWhole = sThankYouFront + "Treasurer & Tax Collector's department" + sThankYouEnd;
                        model.responsibleDepartment = _localizer[sThankYouWhole];
                        model.Department = "Treasurer & Tax Collector's office";
                        model.addressPhoneWebsite = "Los Angeles County Treasurer and Tax Collector<br>" +
                            "225 N. Hill Street, First Floor Lobby<br>" +
                            "Los Angeles, CA 90012<br><br>" +
                            _localizer["Phone"] + ":&nbsp;&nbsp;&nbsp;(888) 807-2111 (" + _localizer["Toll Free"] + ")<br>" +
                            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(213) 974-3211<br><br>" +
                            _localizer["Website"] + ":&nbsp;&nbsp;&nbsp;<a class=\"dont-break-out\" href='http://ttc.lacounty.gov' target='newwin'>http://ttc.lacounty.gov</a><br>";
                        break;

                    case EmailAuditorController:
                        sThankYouWhole = sThankYouFront + "Auditor-Controller's office" + sThankYouEnd;
                        model.responsibleDepartment = _localizer[sThankYouWhole];
                        model.Department = "Auditor-Controller's department";
                        model.addressPhoneWebsite = "Los Angeles County Auditor-Controller<br>" +
                            "Property Tax Services Division<br>" +
                            "500 West Temple Street, Room 153<br>" +
                            "Los Angeles, CA 90012<br><br>" +
                            _localizer["Phone"] + ":&nbsp;&nbsp;&nbsp;(888) 807-2111 (" + _localizer["Toll Free"] + ")<br>" +
                            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(213) 974-8368<br><br>" +
                            _localizer["Website"] + ":&nbsp;&nbsp;&nbsp;<a class=\"dont-break-out\" href='http://auditor.lacounty.gov' target='newwin'>http://auditor.lacounty.gov</a><br>";
                        break;

                    case EmailAssessor:
                        sThankYouWhole = sThankYouFront + "Assessor's office" + sThankYouEnd;
                        model.responsibleDepartment = _localizer[sThankYouWhole];
                        model.Department = "Assessor's department";
                        model.addressPhoneWebsite = "Los Angeles County Assessor's Public Service Section<br>" +
                            "500 West Temple Street, Room 225<br>" +
                            "Los Angeles, CA 90012<br><br>" +
                            _localizer["Phone"] + ":&nbsp;&nbsp;&nbsp;(888) 807-2111 (" + _localizer["Toll Free"] + ")<br>" +
                            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(213) 974-3211<br><br>" +
                            _localizer["Website"] + ":&nbsp;&nbsp;&nbsp;<a class=\"dont-break-out\" href='http://assessor.lacounty.gov' target='newwin'>http://assessor.lacounty.gov</a><br>";
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
                {
                    to = new MailAddress(sEmail);
                }
                else
                {
                    to = new MailAddress(_email.mailTo);
                }

                MailMessage mail = new MailMessage(from, to);
                string sSubject = sSubjectEnglish + " - " + _email.subject + " - Reference # " + sEmailTrackingCount;

                mail.Subject = sSubject;
                mail.IsBodyHtml = _email.isBodyHtml;
                mail.Priority = MailPriority.Normal;
                MailAddress bcc = new MailAddress(_email.bcc);
                mail.Bcc.Add(bcc);

                mail.Body = BuildMailBody("/Templates/EmailTemplate/PublicInquiry.html", model, 0);
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
                        sSubject = _localizer[sSubjectEnglish] + " - " + _localizer[_email.subject] + " - " + _localizer["Reference"] + " # " + sEmailTrackingCount;
                        mail.Subject = sSubject;
                        mail.IsBodyHtml = _email.isBodyHtml;
                        mail.Priority = MailPriority.Normal;
                        mail.Body = BuildMailBody("/Templates/EmailTemplate/PublicInquiryUser.html", model, 1);

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
        /// <param name="iUserEmail"></param>
        /// <returns></returns>
        public string BuildMailBody (string path, PublicInquiryViewModel model, int iUserEmail)
        {
            var wwwRoot = _host.WebRootPath;
            string sOriginalBody = "";

            using (StreamReader reader = new StreamReader(wwwRoot + path))
            {
                sOriginalBody = reader.ReadToEnd();

                if (iUserEmail == 1)
                {
                    sOriginalBody = sOriginalBody.Replace("{PublicInquiryEmailLabel}", _localizer["Public Inquiry Email"]);
                    sOriginalBody = sOriginalBody.Replace("{NotReplyLabel}", _localizer["Please do NOT reply to this email"]);
                    sOriginalBody = sOriginalBody.Replace("{ResponsibleDepartment}", _localizer[model.responsibleDepartment]);
                    sOriginalBody = sOriginalBody.Replace("{ReferenceNumberLabel}", _localizer["The Reference number for your inquiry is"]);
                    sOriginalBody = sOriginalBody.Replace("{NamerLabel}", _localizer["Name"]);
                    sOriginalBody = sOriginalBody.Replace("{BusinessNameLabel}", _localizer["Business Name"]);
                    sOriginalBody = sOriginalBody.Replace("{MailingAddressLabel}", _localizer["Mailing Address"]);
                    sOriginalBody = sOriginalBody.Replace("{EmailLabel}", _localizer["Email"]);
                    sOriginalBody = sOriginalBody.Replace("{DaytimePhoneNumberLabel}", _localizer["Daytime Phone Number"]);
                    sOriginalBody = sOriginalBody.Replace("{FaxNumberLabel}", _localizer["Fax Number"]);
                    sOriginalBody = sOriginalBody.Replace("{PropertyAddressLabel}", _localizer["Property Address"]);
                    sOriginalBody = sOriginalBody.Replace("{AINLabel}", _localizer["Assessor Identification Number"]);
                    sOriginalBody = sOriginalBody.Replace("{CompanyNumberLabel}", _localizer["Company Number"]);
                    sOriginalBody = sOriginalBody.Replace("{RoutingIndexLabel}", _localizer["Routing Index"]);
                    sOriginalBody = sOriginalBody.Replace("{CommentsLabel}", _localizer["Comments"]);
                    sOriginalBody = sOriginalBody.Replace("{SubmitLabel}", _localizer["To submit your inquiries, please use our Public Inquiry Form at"]);
                }
                else
                {
                    sOriginalBody = sOriginalBody.Replace("{Subject}", model.subjectEnglish);
                    sOriginalBody = sOriginalBody.Replace("{Department}", string.IsNullOrEmpty(model.Department) ? "" : model.Department);
                }
  
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
            li.Add(new SelectListItem { Text = _localizer["Please select"], Value = "" });
            foreach (var oneSubject in lSubjects)
            {
                //li.Add(new SelectListItem { Text = oneSubject.Description, Value = oneSubject.Email });
                li.Add(new SelectListItem { Text = _localizer[oneSubject.Description], Value = oneSubject.Description + "|" + oneSubject.Email });
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

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
