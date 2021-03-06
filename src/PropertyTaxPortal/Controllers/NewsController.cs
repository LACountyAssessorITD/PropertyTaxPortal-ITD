﻿//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using PropertyTaxPortal.Models;

//namespace PropertyTaxPortal.Controllers
//{
//    [Authorize]
//    public class NewsController : Controller
//    {
//        private readonly PTPContext _context;

//        public NewsController(PTPContext context)
//        {
//            _context = context;
//        }

//        // GET: News
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.News.ToListAsync());
//        }

//        // GET: News/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var news = await _context.News
//                .FirstOrDefaultAsync(m => m.NewsId == id);
//            if (news == null)
//            {
//                return NotFound();
//            }

//            return View(news);
//        }

//        // GET: News/Create
//        public IActionResult Create()
//        {
//            var news = new News();
//            news.NewsDate = DateTime.Now;
//            news.StartOn = DateTime.Now;
//            news.EndOn = news.StartOn.AddDays(365);

//            return View(news);
//        }

//        // POST: News/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("NewsId,Active,SOrder,Title,Body,Caption,NewsDate,StartOn,EndOn,UploadId")] News news)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(news);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(news);
//        }

//        // GET: News/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var news = await _context.News.FindAsync(id);
//            if (news == null)
//            {
//                return NotFound();
//            }
//            return View(news);
//        }

//        // POST: News/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("NewsId,Active,SOrder,Title,Body,Caption,NewsDate,StartOn,EndOn,UploadId")] News news)
//        {
//            if (id != news.NewsId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(news);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!NewsExists(news.NewsId))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(news);
//        }

//        // GET: News/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var news = await _context.News
//                .FirstOrDefaultAsync(m => m.NewsId == id);
//            if (news == null)
//            {
//                return NotFound();
//            }

//            return View(news);
//        }

//        // POST: News/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var news = await _context.News.FindAsync(id);
//            _context.News.Remove(news);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool NewsExists(int id)
//        {
//            return _context.News.Any(e => e.NewsId == id);
//        }
//    }
//}
