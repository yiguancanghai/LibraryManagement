using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthorApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AuthorApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _context.Authors
                .Include(a => a.Books) // Include related Books
                .ToListAsync();
        }

        // GET: api/AuthorApi/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books) // Include related Books
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound(new { Message = "Author not found." });
            }

            return author;
        }

        // POST: api/AuthorApi
        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(Author author)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(author.FirstName) || string.IsNullOrWhiteSpace(author.LastName))
            {
                return BadRequest(new { Message = "First Name and Last Name are required." });
            }

            // Detach navigation properties from Books to avoid circular references
            if (author.Books != null)
            {
                foreach (var book in author.Books)
                {
                    book.Author = null; // This will avoid the circular reference validation
                    book.LibraryBranch = null; // Detach LibraryBranch if provided
                }
            }

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        // PUT: api/AuthorApi/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest(new { Message = "Author ID mismatch." });
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(author.FirstName) || string.IsNullOrWhiteSpace(author.LastName))
            {
                return BadRequest(new { Message = "First Name and Last Name are required." });
            }

            // Ensure the entity exists
            if (!_context.Authors.Any(a => a.Id == id))
            {
                return NotFound(new { Message = "Author not found." });
            }

            // Update entity
            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { Message = "Concurrency error occurred while updating the author." });
            }
        }

        // DELETE: api/AuthorApi/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books) // Include related Books
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound(new { Message = "Author not found." });
            }

            // Remove author
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/AuthorApi/Books
        [HttpPost("Books")]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            // Validate required fields for Book
            if (string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.ISBN))
            {
                return BadRequest(new { Message = "Book Title and ISBN are required." });
            }

            // Detach navigation properties to avoid circular references
            book.Author = null;
            book.LibraryBranch = null;

            // Add book to the database
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthors), new { id = book.Id }, book);
        }
    }
}