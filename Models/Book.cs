namespace LibraryManagement.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }

        public int AuthorId { get; set; } // Foreign key
        public Author? Author { get; set; } // Make this nullable

        public int? LibraryBranchId { get; set; } // Foreign key, nullable if not every book is associated with a branch
        public LibraryBranch? LibraryBranch { get; set; } // Navigation property for LibraryBranch, nullable if not set
    }
}