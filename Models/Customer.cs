namespace LibraryManagement.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime MembershipDate { get; set; }

        // List of books borrowed by this customer
        public ICollection<BorrowedBook> BorrowedBooks { get; set; }
    }
}