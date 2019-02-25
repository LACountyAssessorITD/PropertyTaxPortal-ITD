using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyTaxPortal.Models;
using ReflectionIT.Mvc.Paging;

namespace PropertyTaxPortal.Controllers
{
    public class FAQsController : Controller
    {
        private readonly PTPContext _context;

        public FAQsController(PTPContext context)
        {
            _context = context;
        }

        // GET: FAQs
        [HttpGet]
        public IActionResult Index()
        {            
            var model = _context.faq.AsNoTracking().OrderBy(f => f.sOrder);
            return View(model);
        }

       
        // GET: FAQs
        [HttpGet]
        public IActionResult View()
        {
            
            //var query = _context.faq.AsNoTracking().OrderBy(f => f.sOrder);
            //var model =  PagingList.CreateAsync(query, 3, page);
            var model = _context.faq.AsNoTracking().OrderBy(f => f.sOrder);
            return PartialView("View", model);
        }


        // GET: FAQs/Create
        public IActionResult CreateorEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new FAQ());
            }
            else
            {
                return View(_context.faq.Find(id));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateorEdit([Bind("FAQID,webSectionID,CategoryID,question,answer,sOrder,updatedOn,featuredCode")] FAQ fAQ)
        {
            if (ModelState.IsValid)
            {
                if (fAQ.FAQID == 0)
                {
                    int faqResults = _context.Database.ExecuteSqlCommand("PTP_createFAQ @p0,@p1,@p2,@p3,@p4,@p5,@p6", fAQ.webSectionID, fAQ.CategoryID, fAQ.question, fAQ.answer, fAQ.sOrder, fAQ.updatedOn, fAQ.featuredCode);
                }
                else
                {
                    int faqResults = _context.Database.ExecuteSqlCommand("PTP_updateFAQ @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7", fAQ.FAQID, fAQ.webSectionID, fAQ.CategoryID, fAQ.question, fAQ.answer, fAQ.sOrder, fAQ.updatedOn, fAQ.featuredCode);
                }
                return RedirectToAction();
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

        // GET: FAQs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int faqResults = _context.Database.ExecuteSqlCommand("PTP_deleteFAQ @p0", id);
            return RedirectToAction(nameof(Index));
        }

        //// POST: FAQs/Delete/5
        //[HttpPost, ActionName("Delete")]  
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var fAQ = await _context.faq.FindAsync(id);
        //    _context.faq.Remove(fAQ);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool FAQExists(int id)
        {
            return _context.faq.Any(e => e.FAQID == id);
        }
    }
}
