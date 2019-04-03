using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyTaxPortal.Models;
using ReflectionIT.Mvc.Paging;

namespace PropertyTaxPortal.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly PTPContext _context;

        public CategoriesController(PTPContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index(int page=1)
        {
            var query = _context.category.FromSql("PTP_getAllCategories").OrderBy(c => c.sOrder);
            var model = await PagingList.CreateAsync(query, 2, page);            
            return View(model);
        }

        public ActionResult DownArrow(int? id, int? pagenum)
        {
            if (id > 0)
            {
                int faqResults = _context.Database.ExecuteSqlCommand("PTP_downArrowSwappingCategories @p0", id);
            }
            return RedirectToAction("Index", new { page = pagenum });
        }

        public ActionResult UpArrow(int? id, int? pagenum)
        {
            if (id > 0)
            {
                int faqResults = _context.Database.ExecuteSqlCommand("PTP_upArrowSwappingCategories @p0", id);
            }
            return RedirectToAction("Index", new { page = pagenum });
        }

        
        // GET: Categories/Create
        public IActionResult CreateorEdit(int id=0)
        {
          
            if (id == 0)
            {
                LoadCategoryTypes();
                return View();
            }
            else
            {
                //LoadCategoryTypes();
                List<RefCode> listRefcodes = new List<RefCode>();
                listRefcodes = _context.refcode.Where(r => r.refCodeType == "CAT").ToList();
                List<SelectListItem> li = new List<SelectListItem>();
                var query = _context.category.Find(id);
                string strRefcode = query.categoryType.ToString();
                foreach (var lref in listRefcodes)
                {
                    if (lref.ReferenceCode == strRefcode)
                    {
                        li.Add(new SelectListItem { Text = lref.Description, Value = lref.ReferenceCode, Selected = true });
                    }
                    else
                    {
                        li.Add(new SelectListItem { Text = lref.Description, Value = lref.ReferenceCode});
                    }
                }
                //var query = _context.category.Find(id);
                ViewBag.CategoryType = li;
                return View(_context.category.Find(id));
            }
        }
        public IActionResult LoadCategoryTypes()
        {
            List<RefCode> listRefcodes = new List<RefCode>();
            listRefcodes = _context.refcode.Where(r => r.refCodeType == "CAT").ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            foreach (var lref in listRefcodes)
            {
                li.Add(new SelectListItem { Text= lref.Description, Value= lref.ReferenceCode });
            }
            ViewBag.CategoryType = li;
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateorEdit([Bind("CategoryID,Descr,categoryType,WebsectionID,sOrder,UploadID")] Category category)
        {
            //if (ModelState.IsValid)
            //{
                if (category.CategoryID == 0)
                {
                    int newsResults = _context.Database.ExecuteSqlCommand("PTP_createCategories @p0,@p1,@p2,@p3,@p4",  category.Descr, category.categoryType,category.WebsectionID,category.sOrder,category.UploadID);
                }
                else
                {
                   int newsResults = _context.Database.ExecuteSqlCommand("PTP_updateCategories @p0,@p1,@p2", category.CategoryID, category.Descr, category.categoryType);
                }
                return RedirectToAction(nameof(Index));
           // }
            //return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryID,Descr,categoryType,WebsectionID,sOrder,UploadID")] Category category)
        {
            if (id != category.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryID))
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
            return View(category);
        }

        //// GET: Categories/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var category = await _context.category
        //        .FirstOrDefaultAsync(m => m.CategoryID == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);
        //}

        // GET: FAQs/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int faqResults = _context.Database.ExecuteSqlCommand("PTP_deleteCategory @p0", id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.category.FindAsync(id);
            _context.category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.category.Any(e => e.CategoryID == id);
        }
    }
}
