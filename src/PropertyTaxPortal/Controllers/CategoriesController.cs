//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using PropertyTaxPortal.Models;
//using ReflectionIT.Mvc.Paging;

//namespace PropertyTaxPortal.Controllers
//{
//    [Authorize]
//    public class CategoriesController : Controller
//    {
//        private readonly PTPContext _context;

//        public CategoriesController(PTPContext context)
//        {
//            _context = context;
//        }

//        // GET: Categories
//        public async Task<IActionResult> Index(int page = 1)
//        {
//            var query = _context.category.FromSql("PTP_getAllCategories").OrderBy(c => c.sOrder);
//            var model = await PagingList.CreateAsync(query, 10, page);
//            return View(model);
//        }

//        // GET: Categories/Create
//        public IActionResult CreateorEdit(int id = 0)
//        {

//            if (id == 0)
//            {
//                ViewBag.Caption = "Create Category";
//                ViewBag.ButtonValue = "Create";
//                LoadCategoryTypes();
//                return View();
//            }
//            else
//            {
//                List<RefCode> listRefcodes = new List<RefCode>();
//                listRefcodes = _context.refcode.Where(r => r.refCodeType == "CAT").ToList();
//                List<SelectListItem> li = new List<SelectListItem>();
//                var query = _context.category.Find(id);
//                string strRefcode = query.categoryType.ToString();
//                foreach (var lref in listRefcodes)
//                {
//                    if (lref.ReferenceCode == strRefcode)
//                    {
//                        li.Add(new SelectListItem { Text = lref.Description, Value = lref.ReferenceCode, Selected = true });
//                    }
//                    else
//                    {
//                        li.Add(new SelectListItem { Text = lref.Description, Value = lref.ReferenceCode });
//                    }
//                }
//                ViewBag.Caption = "Update Category";
//                ViewBag.ButtonValue = "Edit";
//                ViewBag.CategoryType = li;
//                return View(_context.category.Find(id));
//            }
//        }

//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public async Task<IActionResult> CreateorEdit([Bind("CategoryID,Descr,categoryType,WebsectionID,sOrder,UploadID")] Category category)
//        //{
//        //    if (category.CategoryID == 0)
//        //    {
//        //        int newsResults = _context.Database.ExecuteSqlCommand("PTP_createCategories @p0,@p1,@p2,@p3,@p4", category.Descr, category.categoryType, category.WebsectionID, category.sOrder, category.UploadID);
//        //    }
//        //    else
//        //    {
//        //        int newsResults = _context.Database.ExecuteSqlCommand("PTP_updateCategories @p0,@p1,@p2", category.CategoryID, category.Descr, category.categoryType);
//        //    }
//        //    return  RedirectToAction(nameof(Index));
//        //}

//        public ActionResult DownArrow(int? id, int? pagenum)
//        {
//            if (id > 0)
//            {
//                int faqResults = _context.Database.ExecuteSqlCommand("PTP_downArrowSwappingCategories @p0", id);
//            }
//            return RedirectToAction("Index", new { page = pagenum });
//        }

//        public ActionResult UpArrow(int? id, int? pagenum)
//        {
//            if (id > 0)
//            {
//                int faqResults = _context.Database.ExecuteSqlCommand("PTP_upArrowSwappingCategories @p0", id);
//            }
//            return RedirectToAction("Index", new { page = pagenum });
//        }

//        public IActionResult LoadCategoryTypes()
//        {
//            List<RefCode> listRefcodes = new List<RefCode>();
//            listRefcodes = _context.refcode.Where(r => r.refCodeType == "CAT").ToList();
//            List<SelectListItem> li = new List<SelectListItem>();
//            foreach (var lref in listRefcodes)
//            {
//                li.Add(new SelectListItem { Text = lref.Description, Value = lref.ReferenceCode });
//            }
//            ViewBag.CategoryType = li;
//            return View();
//        }

//        [HttpGet]
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//            int faqResults = _context.Database.ExecuteSqlCommand("PTP_deleteCategory @p0", id);
//            return RedirectToAction(nameof(Index));
//        }


//        private bool CategoryExists(int id)
//        {
//            return _context.category.Any(e => e.CategoryID == id);
//        }
//    }
//}
