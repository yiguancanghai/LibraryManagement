using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;

        public AuthorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Author
        public async Task<IActionResult> Index()
        {
            var authors = await _context.Authors.ToListAsync();
            return View(authors);
        }

        // GET: Author/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Author/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(author);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Author created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "Validation failed.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while creating the author.";
                Console.WriteLine(ex.Message); // Log the exception
            }
            return View(author);
        }

        // GET: Author/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Author/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Author updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Authors.Any(a => a.Id == id))
                    {
                        return NotFound();
                    }

                    throw;
                }
            }

            return View(author);
        }

        // GET: Author/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Author/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Author deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}