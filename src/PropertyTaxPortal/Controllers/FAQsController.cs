using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyTaxPortal.Models;
using ReflectionIT.Mvc.Paging;
using NLog;

namespace PropertyTaxPortal.Controllers
{
    public class FAQsController : Controller
    {
        private readonly PTPContext _context;
        private static Logger fileLogger = LogManager.GetLogger("fileLogger");
        private static Logger databaseLogger = LogManager.GetLogger("databaseLogger");

        public FAQsController(PTPContext context)
        {
            _context = context;
        }

        // GET: FAQs
        public async Task<IActionResult> Index(int page=1)
        {
            try
            {
                var query = _context.faq.AsNoTracking().OrderBy(f => f.FAQID);
                var model = await PagingList.CreateAsync(query, 2, page);
                return View(model);

            }
            catch (Exception ex)
            {
                fileLogger.Error(ex, "Error occured in connecting to the database");
                return View("~/Views/Shared/_ErrorMessage.cshtml");
            }
        }

        //// GET: FAQs/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var fAQ = await _context.faq
        //        .FirstOrDefaultAsync(m => m.FAQID == id);
        //    if (fAQ == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(fAQ);
        //}

        // GET: FAQs/Create
        public IActionResult CreateorEdit(int id=0)
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

        // POST: FAQs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateorEdit([Bind("FAQID,webSectionID,CategoryID,question,answer,sOrder,updatedOn,featuredCode")] FAQ fAQ)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(fAQ);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                if (fAQ.FAQID == 0)
                {
                    int faqResults = _context.Database.ExecuteSqlCommand("PTP_createFAQ @p0,@p1,@p2,@p3,@p4,@p5,@p6", fAQ.webSectionID, fAQ.CategoryID, fAQ.question, fAQ.answer, fAQ.sOrder, fAQ.updatedOn, fAQ.featuredCode);
                }
                else
                {
                    int faqResults = _context.Database.ExecuteSqlCommand("PTP_updateFAQ @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7", fAQ.FAQID, fAQ.webSectionID, fAQ.CategoryID, fAQ.question, fAQ.answer, fAQ.sOrder, fAQ.updatedOn, fAQ.featuredCode);
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

        // POST: FAQs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FAQID,webSectionID,CategoryID,question,answer,sOrder,updatedOn,featuredCode")] FAQ fAQ)
        {
            if (id != fAQ.FAQID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fAQ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FAQExists(fAQ.FAQID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fAQ);
        }

        // GET: FAQs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fAQ = await _context.faq
                .FirstOrDefaultAsync(m => m.FAQID == id);
            if (fAQ == null)
            {
                return NotFound();
            }

            return View(fAQ);
        }

        // POST: FAQs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fAQ = await _context.faq.FindAsync(id);
            _context.faq.Remove(fAQ);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FAQExists(int id)
        {
            return _context.faq.Any(e => e.FAQID == id);
        }
    }
}
