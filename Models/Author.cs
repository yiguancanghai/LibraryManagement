using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        // Navigation property for a one-to-many relationship
        public ICollection<Book> Books { get; set; } = new List<Book>(); // Initialized to avoid null references
    }
}