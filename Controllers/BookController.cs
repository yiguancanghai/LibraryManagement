using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Data;
using LibraryManagement.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            // Populate dropdown lists with authors and library branches
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.LibraryBranches = _context.LibraryBranches.ToList();
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                // Add new book to the database
                _context.Books.Add(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // If model state is invalid, repopulate the dropdowns
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.LibraryBranches = _context.LibraryBranches.ToList();
            return View(book);
        }

        // GET: Book/Index
        public IActionResult Index()
        {
            // Retrieve all books from the database, including related Author and LibraryBranch information
            var books = _context.Books
                .Include(b => b.Author)
                .Include(b => b.LibraryBranch)
                .ToList();
            return View(books);
        }

        // GET: Book/Edit/{id}
        public IActionResult Edit(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            // Populate dropdown lists with authors and library branches
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.LibraryBranches = _context.LibraryBranches.ToList();
            return View(book);
        }

        // POST: Book/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // If model state is invalid, repopulate the dropdowns
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.LibraryBranches = _context.LibraryBranches.ToList();
            return View(book);
        }

        // GET: Book/Delete/{id}
        public IActionResult Delete(int id)
        {
            var book = _context.Books
                .Include(b => b.Author)
                .Include(b => b.LibraryBranch)
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Book/Details/{id}
        public IActionResult Details(int id)
        {
            var book = _context.Books
                .Include(b => b.Author)
                .Include(b => b.LibraryBranch)
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}