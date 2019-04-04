using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyTaxPortal.Models;
using ReflectionIT.Mvc.Paging;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;

namespace PropertyTaxPortal.Controllers
{
    [Authorize]
    public class FAQsController : Controller
    {
        private readonly PTPContext _context;

        public FAQsController(PTPContext context)
        {
            _context = context;
        }

        // GET: FAQs
        
        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _context.faq.FromSql("PTP_getAllFAQs").OrderBy(f => f.sOrder);
            var model = await PagingList.CreateAsync(query, 10, page);
            return View(model);
        }

        //// GET: FAQs
        //[HttpGet]
        //public IActionResult View()
        //{

        //    //var query = _context.faq.AsNoTracking().OrderBy(f => f.sOrder);
        //    //var model =  PagingList.CreateAsync(query, 3, page);
        //    var model = _context.faq.AsNoTracking().OrderBy(f => f.sOrder);
        //    return PartialView("View", model);
        //}


        // GET: FAQs/Create
       
        public IActionResult CreateorEdit(int id = 0)
        {
            if (id == 0)
            {
                LoadFAQCategoryTypes();
                return View(new FAQ());
            }
            else
            {
                List<Category> listCatcodes = new List<Category>();
                listCatcodes = _context.category.Where(c => c.categoryType == "FQC").ToList();
                List<SelectListItem> li = new List<SelectListItem>();
                //var query = _context.Database.ExecuteSqlCommand("PTP_getAllFAQsbyCategoryID @p0", fa);
                var query = _context.faq.FromSql("PTP_getAllFAQsbyCategoryID @p0", id).OrderBy(f => f.sOrder);
                string strCatID = query.SingleOrDefault().CategoryID.ToString();
                foreach (var lcat in listCatcodes)
                {
                    if (Convert.ToString(lcat.CategoryID) == strCatID)
                    {
                        li.Add(new SelectListItem { Text = lcat.Descr, Value = Convert.ToString(lcat.CategoryID), Selected = true });
                    }
                    else
                    {
                        li.Add(new SelectListItem { Text = lcat.Descr, Value = Convert.ToString(lcat.CategoryID) });
                    }
                }
                ViewBag.CategoryID = li;
                return View(_context.faq.Find(id));
            }
        }
        
        public IActionResult LoadFAQCategoryTypes()
        {
            List<Category> listCatcodes = new List<Category>();
            //var query = _context.Database.ExecuteSqlCommand("PTP_getCategoriesForFAQ");

            listCatcodes=_context.category.Where(c => c.categoryType == "FQC").ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            foreach (var lcat in listCatcodes)
            {
                li.Add(new SelectListItem { Text = lcat.Descr, Value =Convert.ToString(lcat.CategoryID) });
            }
            ViewBag.CategoryID = li;
            return View();
        }

        //public IActionResult LoadFAQFeaturedCode()
        //{
        //    List<FAQ> listFeaturedcodes = new List<FAQ>();
        //    //var query = _context.Database.ExecuteSqlCommand("PTP_getCategoriesForFAQ");

        //    listFeaturedcodes = _context.faq.Where(c => c.categoryType == "FQC").ToList();
        //    List<SelectListItem> li = new List<SelectListItem>();
        //    foreach (var lcat in listCatcodes)
        //    {
        //        li.Add(new SelectListItem { Text = lcat.Descr, Value = Convert.ToString(lcat.CategoryID) });
        //    }
        //    ViewBag.CategoryID = li;
        //    return View();
        //}

        //public IActionResult AllFAQs()
        //{
        //    dynamic FaqFinalModel = new ExpandoObject();
        //    var modelTaxBillFAQs = _context.faq.FromSql("PTP_getAllFAQs").Where(f=>f.CategoryID==1).OrderBy(f => f.sOrder);
        //    var modelRefundFAQs = _context.faq.FromSql("PTP_getAllFAQs").Where(f => f.CategoryID == 2).OrderBy(f => f.sOrder);
        //    FaqFinalModel.TaxBill = modelTaxBillFAQs;
        //    FaqFinalModel.Refund = modelRefundFAQs;
        //    return View(FaqFinalModel);
        //}

        

        public IActionResult FAQTabs()
        {
            dynamic FaqFinalModel = new ExpandoObject();
            var modelTaxBillFAQs = _context.faq.FromSql("PTP_getAllFAQs").Where(f => f.CategoryID == 1).OrderBy(f => f.sOrder);
            var modelRefundFAQs = _context.faq.FromSql("PTP_getAllFAQs").Where(f => f.CategoryID == 2).OrderBy(f => f.sOrder);
            var modelPropertyFAQs = _context.faq.FromSql("PTP_getAllFAQs").Where(f => f.CategoryID == 3).OrderBy(f => f.sOrder);
            var modelOwnershipFAQs = _context.faq.FromSql("PTP_getAllFAQs").Where(f => f.CategoryID == 4).OrderBy(f => f.sOrder);
            FaqFinalModel.TaxBill = modelTaxBillFAQs;
            FaqFinalModel.Refund = modelRefundFAQs;
            FaqFinalModel.Property = modelPropertyFAQs;
            FaqFinalModel.Ownership = modelOwnershipFAQs;
            return View(FaqFinalModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateorEdit([Bind("FAQID,webSectionID,CategoryID,question,answer,sOrder,updatedOn,featuredCode")] FAQ fAQ)
        {
            if (ModelState.IsValid)
            {
                if (fAQ.FAQID == 0)
                {
                    int faqResults = _context.Database.ExecuteSqlCommand("PTP_createFAQ @p0,@p1,@p2,@p3,@p4,@p5,@p6", fAQ.webSectionID, fAQ.CategoryID, fAQ.question, fAQ.answer, fAQ.sOrder, DateTime.Now, fAQ.featuredCode);
                }
                else
                {
                    int faqResults = _context.Database.ExecuteSqlCommand("PTP_updateFAQ @p0,@p1,@p2,@p3,@p4,@p5,@p6", fAQ.FAQID, fAQ.webSectionID, fAQ.CategoryID, fAQ.question, fAQ.answer, fAQ.updatedOn, fAQ.featuredCode);
                }
                return RedirectToAction(nameof(Index)); 
            }
            return View(fAQ);
        }

        // GET: FAQs/Edit/5
       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fAQ = await _context.faq.FindAsync(id);
            if (fAQ == null)
            {
                return NotFound();
            }
            return View(fAQ);
        }

        
        [HttpPost]
        public ActionResult UpArrow(int? id)
        {
            if (id > 0)
            {
                int faqResults = _context.Database.ExecuteSqlCommand("PTP_upArrowSwapping @p0", id);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult DownArrow(int? id)
        {
            if (id > 0)
            {
                int faqResults = _context.Database.ExecuteSqlCommand("PTP_downArrowSwapping @p0", id);
            }
            //int pageNumber = Convert.ToInt16(HttpContext.Request.Query["page"]);
            return RedirectToAction(nameof(Index));
        }        

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int faqResults = _context.Database.ExecuteSqlCommand("PTP_deleteFAQ @p0", id);
            return RedirectToAction(nameof(Index));
        }       

        private bool FAQExists(int id)
        {
            return _context.faq.Any(e => e.FAQID == id);
        }
    }
}
