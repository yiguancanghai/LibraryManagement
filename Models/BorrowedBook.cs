namespace LibraryManagement.Models
{
    public class BorrowedBook
    {
        public int Id { get; set; }  // This is the primary key

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}