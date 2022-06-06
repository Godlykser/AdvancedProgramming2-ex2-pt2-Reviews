using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reviews.Data;
using Reviews.Models;
using Reviews.Services;

namespace Reviews.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ReviewsContext _context;
        private readonly IReviewService _reviewService;

        public ReviewsController(ReviewsContext context)
        {
            _context = context;
            _reviewService = new ReviewService();
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            ViewBag.AverageScore = _context.Review.Average(r => r.Score).ToString("0.00");
            return _context.Review != null ?
                        View(await _context.Review.ToListAsync()) :
                        Problem("Entity set 'ReviewsContext.Review'  is null.");
        }

        public async Task<IActionResult> Search(string query)
        {
            if (_context.Review == null)
            {
                return NotFound();
            }
            var q = (query != null) ?
                _context.Review.Where(r => r.Username.Contains(query) || r.Feedback.Contains(query)) :
                _context.Review;

            return Json(await q.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Review == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Score,Feedback")] Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Review == null)
            {
                return NotFound();
            }

            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Score,Feedback")] Review review)
        {
            if (id != review.Id || _context.Review == null)
            {
                return NotFound();
            }
            var reviewToUpdate = await _context.Review.FindAsync(id);
            if (ModelState.IsValid && reviewToUpdate != null)
            {
                try
                {
                    reviewToUpdate.Score = review.Score;
                    reviewToUpdate.Feedback = review.Feedback;
                    _context.Update(reviewToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
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
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Review == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Review == null)
            {
                return Problem("Entity set 'ReviewsContext.Review'  is null.");
            }
            var review = await _context.Review.FindAsync(id);
            if (review != null)
            {
                _context.Review.Remove(review);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return (_context.Review?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
