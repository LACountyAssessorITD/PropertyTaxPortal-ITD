using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyTaxPortal.Models;
using ReflectionIT.Mvc.Paging;
using System.Web;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;

namespace PropertyTaxPortal.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private readonly PTPContext _context;

        public NewsController(PTPContext context)
        {
            _context = context;
        }

        // GET: News
        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _context.News.FromSql("PTP_getAllNews").OrderBy(n => n.sOrder);
            var model = await PagingList.CreateAsync(query, 2, page);
            return View(model);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            return View();
        }

        public ActionResult DownArrow(int? id, int? pagenum)
        {
            if (id > 0)
            {
                int faqResults = _context.Database.ExecuteSqlCommand("PTP_downArrowSwappingNews @p0", id);
            }
            return RedirectToAction("Index", new { page = pagenum });
        }

        public ActionResult UpArrow(int? id, int? pagenum)
        {
            if (id > 0)
            {
                int faqResults = _context.Database.ExecuteSqlCommand("PTP_upArrowSwappingNews @p0", id);
            }
            return RedirectToAction("Index", new { page = pagenum });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateorEdit([Bind("NewsID,WebSectionID,UploadID,Title,Body,Caption,FeaturedCode,NewsDate,Starton,EndOn,isGlobal,Image")] NEWS news)
        {
            if (ModelState.IsValid)
            {
                if (news.NewsID == 0)
                {
                    int newsResults = _context.Database.ExecuteSqlCommand("PTP_createNews @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10", news.WebSectionID, news.UploadID, news.Title, news.Body, news.Caption, news.sOrder, news.FeaturedCode, news.NewsDate, news.Starton, news.EndOn, news.isGlobal);
                }
                else
                {
                    int newsResults = _context.Database.ExecuteSqlCommand("PTP_updateNews @p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9", news.NewsID, 1824, news.Title, news.Body, news.Caption, news.FeaturedCode, news.NewsDate, news.Starton, news.EndOn, 1);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> CreateorEdit(int? id)
        {

            if (id == null)
            {
                return View();
            }
            else
            {
                var newss = await _context.News.FindAsync(id);
                if (newss == null)
                {
                    return NotFound();
                }
                return View(newss);
            }
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewsID,WebSectionID,UploadID,Title,Body,Caption,FeaturedCode,NewsDate,Starton,EndOn,isGlobal,Displayed")] NEWS news)
        {
            if (id != news.NewsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.NewsID))
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
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.NewsID == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.NewsID == id);
        }

        public ActionResult LoadFeaturedCodes()
        {
            List<SelectListItem> li = new List<SelectListItem>() {
                new SelectListItem {
                    Text = "Featured", Value = "NFE"
                },
                new SelectListItem {
                    Text = "Current", Value = "NCE"
                },
                new SelectListItem {
                    Text = "Archived", Value = "NAE"
                },
            };
            ViewData["FeaturedCodes"] = li;
            return View();
        }
    }
}
