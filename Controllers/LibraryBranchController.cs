using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class LibraryBranchController : Controller
    {
        private readonly AppDbContext _context;

        public LibraryBranchController(AppDbContext context)
        {
            _context = context;
        }

        // GET: LibraryBranch/Index
        public async Task<IActionResult> Index()
        {
            var branches = await _context.LibraryBranches
                                         .Select(b => new LibraryBranchViewModel
                                         {
                                             Id = b.Id,
                                             BranchName = b.BranchName,
                                             Address = b.Address,
                                             Phone = b.Phone,
                                             BookTitles = b.Books.Select(book => book.Title).ToList()
                                         }).ToListAsync();

            return View(branches);
        }

        // GET: LibraryBranch/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var branch = await _context.LibraryBranches
                                       .Include(b => b.Books)
                                       .Where(b => b.Id == id)
                                       .Select(b => new LibraryBranchViewModel
                                       {
                                           Id = b.Id,
                                           BranchName = b.BranchName,
                                           Address = b.Address,
                                           Phone = b.Phone,
                                           BookTitles = b.Books.Select(book => book.Title).ToList()
                                       }).FirstOrDefaultAsync();

            if (branch == null) return NotFound();

            return View(branch);
        }

        // GET: LibraryBranch/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LibraryBranch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LibraryBranchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var branch = new LibraryBranch
                {
                    BranchName = model.BranchName,
                    Address = model.Address,
                    Phone = model.Phone
                };
                _context.LibraryBranches.Add(branch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: LibraryBranch/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var branch = await _context.LibraryBranches.FindAsync(id);
            if (branch == null) return NotFound();

            var model = new LibraryBranchViewModel
            {
                Id = branch.Id,
                BranchName = branch.BranchName,
                Address = branch.Address,
                Phone = branch.Phone
            };
            return View(model);
        }

        // POST: LibraryBranch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LibraryBranchViewModel model)
        {
            if (id != model.Id) return NotFound();

           
                try
                {
                    var branch = await _context.LibraryBranches.FindAsync(id);
                    if (branch == null) return NotFound();

                    branch.BranchName = model.BranchName;
                    branch.Address = model.Address;
                    branch.Phone = model.Phone;

                    _context.Update(branch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibraryBranchExists(model.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            
            return View(model);
        }

        // GET: LibraryBranch/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var branch = await _context.LibraryBranches
                                       .Select(b => new LibraryBranchViewModel
                                       {
                                           Id = b.Id,
                                           BranchName = b.BranchName,
                                           Address = b.Address,
                                           Phone = b.Phone
                                       }).FirstOrDefaultAsync(m => m.Id == id);

            if (branch == null) return NotFound();

            return View(branch);
        }

        // POST: LibraryBranch/Delete/5
        [HttpPost ]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branch = await _context.LibraryBranches.FindAsync(id);
            if (branch == null) return NotFound();

            _context.LibraryBranches.Remove(branch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryBranchExists(int id)
        {
            return _context.LibraryBranches.Any(e => e.Id == id);
        }
    }
}